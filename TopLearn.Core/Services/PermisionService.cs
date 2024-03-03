using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class PermisionService : IPermisionService
    {

        #region Dependency Injection

        private readonly TopLearnContext _context;

        public PermisionService(TopLearnContext context)
        {
            _context = context;
        }

        #endregion

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
