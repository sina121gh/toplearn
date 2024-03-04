using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TopLearn.Core.Convertors;
using TopLearn.Core.DTOs;
using TopLearn.Core.Generator;
using TopLearn.Core.Security;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Transaction = TopLearn.DataLayer.Entities.Wallet.Transaction;

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

        public User? GetUserForLogin(LoginViewModel login)
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
                Wallet = GetWalletBalance(userName),
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

            if (profile.UserAvatar?.Length > 0)
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

        public bool ChangeUserSalt(string userName)
        {
            try
            {
                User user = GetUserByUserName(userName);
                user.Salt = PasswordHelper.GenerateSalt();
                UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeUserPassword(string userName, string newPassword)
        {
            try
            {
                User user = GetUserByUserName(userName);
                user.Password = PasswordHelper.HashPassword(newPassword, user.Salt);
                UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string? GetUserPassword(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).Password;
        }

        public int GetWalletBalance(string userName)
        {
            int userId = GetUserIdByUserName(userName);

            var incomes = _context.Transactions
                .Where(t => t.UserId == userId && t.TypeId == 1 && t.IsSuccess)
                .Select(t => t.Amount)
                .ToList();

            var outcomes = _context.Transactions
                .Where(t => t.UserId == userId && t.TypeId == 2 && t.IsSuccess)
                .Select(t => t.Amount)
                .ToList();

            return incomes.Sum() - outcomes.Sum();
        }

        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).Id;
        }

        public IEnumerable<TransactionsListViewModel> GetTransactions(string userName)
        {
            int userId = GetUserIdByUserName(userName);
            var transactions = _context.Transactions
                .OrderByDescending(t => t.CreateDate)
                .Where(t => t.UserId == userId)
                .ToList();

            return transactions.Select(t =>
            new TransactionsListViewModel()
            {
                Type = t.TypeId == 1 ? true : false,
                Amout = t.Amount,
                Description = t.Description,
                Date = t.CreateDate,
                Status = t.IsSuccess,
            }).ToList();
        }

        public int ChargeWallet(string userName, int amount, string description, bool isSuccess = false)
        {
            Transaction transaction = new Transaction()
            {
                UserId = GetUserIdByUserName(userName),
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsSuccess = isSuccess,
                TypeId = 1,
            };

            try
            {
                return AddTransaction(transaction);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int AddTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return transaction.Id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Transaction? GetTransactionById(int transactionId)
        {
            return _context.Transactions.Find(transactionId);
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Update(transaction);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UsersForAdminViewModel GetUsers(int take = 10, int pageId = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrEmpty(filterEmail))
                result = result.Where(u => u.Email.Contains(filterEmail));

            if (!string.IsNullOrEmpty(filterUserName))
                result = result.Where(u => u.UserName.Contains(filterUserName));

            // Show Item In Page
            int skip = (pageId - 1) * take;

            UsersForAdminViewModel viewModel = new UsersForAdminViewModel()
            {
                CurrentPage = pageId,
                PageCount = (int)Math.Ceiling(result.Count() / (double)take),
                Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take).ToList(),
            };
            viewModel.HasNextPage = viewModel.CurrentPage < viewModel.PageCount;
            viewModel.HasPreviousPage = viewModel.CurrentPage > 1;

            return viewModel;
        }

        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User newUser = new User()
            {
                Email = user.Email,
                UserName = user.UserName,
                Salt = PasswordHelper.GenerateSalt(),
                IsActive = user.IsActive,
                RegisterDate = DateTime.Now,
                ActiveCode = MyGenerator.GenerateCode(),
            };

            newUser.Password = PasswordHelper.HashPassword(user.Password, newUser.Salt);

            #region Save User Avatar

            if (user.UserAvater != null)
            {
                string avatar = MyGenerator.GenerateCode() + Path.GetExtension(user.UserAvater.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UsersAvatars",
                        avatar);
                using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvater.CopyTo(stream);
                }

                newUser.Avatar = avatar;
            }
            else
            {
                newUser.Avatar = "DefaultAvatar.png";
            }

            #endregion

            return AddUser(newUser);

            
        }
    }
}
