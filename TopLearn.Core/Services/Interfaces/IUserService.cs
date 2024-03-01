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
        int AddUser(User user);
        int GetUserIdByUserName(string userName);
        User? GetUserByEmail(string email);
        User? GetUserForLogin(LoginViewModel login);
        User? GetUserByActiveCode(string activeCode);
        User? GetUserByUserName(string userName);

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

        int ChargeWallet(string userName, int amount, string description, bool isSuccess = false);
        int AddTransaction(Transaction transaction);
        int GetWalletBalance(string userName);
        Transaction? GetTransactionById(int transactionId);
        bool UpdateTransaction(Transaction transaction);
        IEnumerable<TransactionsListViewModel> GetTransactions(string userName);

        #endregion

        #region Admin Panel

        UsersForAdminViewModel GetUsers(int take = 10, int pageId = 1, string filterEmail = "", string filterUserName = "");

        #endregion
    }
}
