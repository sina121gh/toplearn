using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class UserService : IUserService
    {
        private readonly TopLearnContext _context;

        public UserService(TopLearnContext context)
        {
            _context = context;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User? LoginUser(LoginViewModel login)
        {
            User user = GetUserByEmail(FixedText.FixEmail(login.Email));

            if (user != null)
                if (PasswordHelper.VerifyPassword(login.Password, user.Password))
                    return user;

            return null;
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);

            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = MyGenerator.GenerateCode();
            _context.SaveChanges();

            return true;
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User? GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public UserInformationViewModel GetUserInformation(string userName)
        {
            User user = GetUserByUserName(userName);

            UserInformationViewModel userInfo = new UserInformationViewModel()
            {
                UserName = userName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                Wallet = 0,
            };

            return userInfo;
        }

        public User? GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(u => u.UserName == userName);
        }

        public UserPanelSideBarViewModel GetUserPanelSideBar(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName)
                .Select(u => new UserPanelSideBarViewModel()
                {
                    UserName = u.UserName,
                    RegisterDate = u.RegisterDate,
                    PitcureName = u.Avatar
                }).Single();
        }

        public EditProfileViewModel GetUserForEdit(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName)
                .Select(u => new EditProfileViewModel()
                {
                    UserName = u.UserName,
                    Email = u.Email,
                }).Single();
        }

        public bool EditProfile(string userName, EditProfileViewModel profile)
        {
            User user = GetUserByUserName(userName);

            if (user == null) return false;

            if(profile.UserAvatar ? .Length > 0)
            {
                string imagePath = "";
                string currentAvatar = user.Avatar;
                if (currentAvatar != "DefaultAvatar.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UsersAvatars",
                        currentAvatar);
                    if (File.Exists(imagePath))
                        File.Delete(imagePath);
                }

                string newAvatar = MyGenerator.GenerateCode() + Path.GetExtension(profile.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UsersAvatars",
                        newAvatar);
                using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                {
                    profile.UserAvatar.CopyTo(stream);
                }

                user.Avatar = newAvatar;
            }

            user.UserName = profile.UserName;
            user.Email = profile.Email;

            try
            {
                UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}
