using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool ActiveAccount(string activeCode);
        bool UpdateUser(User user);
        int AddUser(User user);
        User? GetUserByEmail(string email);
        User? LoginUser(LoginViewModel login);
        User? GetUserByActiveCode(string activeCode);
        User? GetUserByUserName(string userName);

        #region User Panel

        UserInformationViewModel GetUserInformation(string userName);
        UserPanelSideBarViewModel GetUserPanelSideBar(string userName);
        EditProfileViewModel GetUserForEdit(string userName);
        bool EditProfile(string userName, EditProfileViewModel profile);

        #endregion
    }
}
