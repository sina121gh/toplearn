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
        private readonly TopLearnContext _context;
        private readonly IUserService _userService;

        public PermisionService(TopLearnContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;

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

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
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

        public bool HasUserThisRole(int userId, int roleId)
        {
            return _context.UserRoles.Any(ur => ur.UserId == userId && ur.RoleId == roleId);
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
    }
}
