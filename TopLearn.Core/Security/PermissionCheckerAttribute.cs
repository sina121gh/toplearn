using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermisionService _permisionService;
        int _permissionId = 0;
        string _permissionTitle = "";
        public PermissionCheckerAttribute(string permissionTitle)
        {
            _permissionTitle = permissionTitle;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permisionService = (IPermisionService)context.HttpContext.RequestServices.GetService(typeof(IPermisionService));

            _permissionId = _permisionService.GetPermissionIdByTitle(_permissionTitle);

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                if (_permissionId != 0)
                {
                    string userName = context.HttpContext.User.Identity.Name;

                    if (!_permisionService.HasUserThisPermission(_permissionId, userName))
                        context.Result = new RedirectResult("/login/");
                }
                else
                {
                    context.Result = new RedirectResult("/error/");
                }
                    

            }
            else
                context.Result = new RedirectResult("/login/");
        }
    }
}
