﻿using System;
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
        IEnumerable<Role> GetUserRoles(int userId);
        bool AddRolesToUser(IEnumerable<int> rolesIds, int userId);
        bool HasUserThisRole(int userId, int roleId);
        bool EditUserRoles(int userId, IEnumerable<int> rolesIds);
        bool DeleteUserRoles(int userId);

        #endregion
    }
}