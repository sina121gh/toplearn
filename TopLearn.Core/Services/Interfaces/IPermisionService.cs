using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IPermisionService
    {
        #region Roles

        IEnumerable<Role> GetRoles();

        #endregion
    }
}
