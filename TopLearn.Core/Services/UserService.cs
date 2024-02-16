using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;

namespace TopLearn.Core.Services
{
    public class UserService : IUserService
    {
        private readonly TopLearnContext _context;

        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        public bool IsExistsEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistsUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }
    }
}
