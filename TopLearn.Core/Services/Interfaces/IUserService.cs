using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region User

        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool ActiveAccount(string activeCode);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        int AddUser(User user);
        int GetUserIdByUserName(string userName);
        User? GetUserByEmail(string email);
        User? GetUserForLogin(LoginViewModel login);
        User? GetUserByActiveCode(string activeCode);
        User? GetUserByUserName(string userName);
        User? GetUserById(int userId);

        #endregion

        #region User Panel

        UserInformationViewModel GetUserInformation(string userName);
        UserPanelSideBarViewModel GetUserPanelSideBar(string userName);
        EditProfileViewModel GetUserForEdit(string userName);
        bool EditProfile(string userName, EditProfileViewModel profile);
        bool ChangeUserSalt(string userName);
        bool ChangeUserPassword(string userName, string newPassword);
        string? GetUserPassword(string userName);

        #endregion

        #region Wallet

        int WithdrawWallet(string userName, int amount, string description, bool isSuccess = true);
        int ChargeWallet(string userName, int amount, string description, bool isSuccess = false);
        int AddTransaction(Transaction transaction);
        int GetWalletBalance(string userName);
        int CalculateWalletBalance(string userName);
        Transaction? GetTransactionById(int transactionId);
        bool UpdateTransaction(Transaction transaction);
        bool UpdateWalletBalance(Transaction transaction);
        bool UpdateWallet(Wallet wallet);
        bool CreateWallet(int userId);
        bool AddWallet(Wallet wallet);
        Wallet GetWalletByUserId(int userId);
        IEnumerable<TransactionsListViewModel> GetTransactions(string userName);

        #endregion

        #region Admin Panel

        UsersForAdminViewModel GetUsers(int take = 10, int pageId = 1, string filterEmail = "", string filterUserName = "");
        UsersForAdminViewModel GetDeletedUsers(int take = 10, int pageId = 1, string filterEmail = "", string filterUserName = "");
        EditUserViewModel GetUserForEdit(int userId);
        int AddUserFromAdmin(CreateUserViewModel user);
        bool EditUserFromAdmin(EditUserViewModel editUser);

        #endregion
    }
}
