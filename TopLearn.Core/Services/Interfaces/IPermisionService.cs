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

        Role GetRoleById(int roleId);
        IEnumerable<Role> GetRoles();
        IEnumerable<Role> GetUserRoles(int userId);
        bool AddRolesToUser(IEnumerable<int> rolesIds, int userId);
        bool HasUserThisRole(int userId, int roleId);
        bool EditUserRoles(int userId, IEnumerable<int> rolesIds);
        bool UpdateRole(Role role);
        bool DeleteUserRoles(int userId);
        bool DeleteRole(int roleId);
        int AddRole(Role role);

        #endregion
    }
}
