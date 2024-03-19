using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Permissions;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class PermisionService : IPermisionService
    {
        private readonly TopLearnContext _context;
        private readonly IUserService _userService;

        public PermisionService(TopLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;

        }

        public bool AddPermissionsToRole(int roleId, IEnumerable<int> permissions)
        {
            foreach (int permissionId in permissions)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    PermissionId = permissionId,
                    RoleId = roleId
                });
            }
            return _context.SaveChanges() > 0;
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.Id;
        }

        public bool AddRolesToUser(IEnumerable<int> rolesIds, int userId)
        {
            foreach (int roleId in rolesIds)
            {
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = roleId,
                    UserId = userId,
                });
            }

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteRole(int roleId)
        {
            Role role = GetRoleById(roleId);

            if (role != null)
            {
                try
                {
                    _context.Roles.Remove(role);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        public bool DeleteRolePermissions(int roleId)
        {
            try
            {
                _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToList().ForEach(rp => _context.Remove(rp));

                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUserRoles(int userId)
        {
            try
            {
                _context.UserRoles.Where(r => r.UserId == userId)
                .ToList()
                .ForEach(r => _context.UserRoles.Remove(r));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditUserRoles(int userId, IEnumerable<int> rolesIds)
        {
            try
            {
                DeleteUserRoles(userId);
                AddRolesToUser(rolesIds, userId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetPermissionIdByTitle(string permissionTitle)
        {
            Permission permission = _context.Permissions
                .SingleOrDefault(p => p.Title == permissionTitle);

            if (permission != null)
                return permission.Id;

            return 0;
        }

        public ICollection<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public IEnumerable<int> GetRolesIdsByPermission(int permissionId)
        {
            return _context.RolePermissions
                .Where(rp => rp.PermissionId == permissionId)
                .Select(rp => rp.RoleId)
                .ToList();
        }

        public IEnumerable<Role> GetUserRoles(int userId)
        {
            return _userService.GetUserById(userId).UserRoles
                .Select(r => new Role()
                {
                    Id = r.Role.Id,
                    Title = r.Role.Title,
                });
        }

        public IEnumerable<int> GetUserRolesIds(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);

            return _context.UserRoles
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleId)
                .ToList();
        }

        public bool HasPermissionChildren(int permissionId)
        {
            return _context.Permissions.Any(p => p.ParentId == permissionId);
        }

        public bool HasRoleThisPermission(int roleId, int permissionId)
        {
            return _context.RolePermissions
                .Any(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }

        public bool HasUserThisPermission(int permissionId, string userName)
        {
            List<int> userRolesIds = GetUserRolesIds(userName).ToList();

            if (!userRolesIds.Any())
                return false;

            IEnumerable<int> acceptedRolesIds = GetRolesIdsByPermission(permissionId);

            return acceptedRolesIds
                .Any(r => userRolesIds.Contains(r));
        }

        public bool HasUserThisRole(int userId, int roleId)
        {
            return _context.UserRoles.Any(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public IEnumerable<int> RolePermissionsIds(int roleId)
        {
            return _context.RolePermissions
                 .Where(rp => rp.RoleId == roleId)
                 .Select(rp => rp.PermissionId)
                 .ToList();
        }

        public bool UpdateRole(Role role)
        {
            try
            {
                _context.Roles.Update(role);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateRolePermissions(int roleId, IEnumerable<int> permissionsIds)
        {
            try
            {
                DeleteRolePermissions(roleId);
                AddPermissionsToRole(roleId, permissionsIds);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
