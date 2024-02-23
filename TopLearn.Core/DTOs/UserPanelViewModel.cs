using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs
{
    public class UserInformationViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public uint Wallet { get; set; }
    }

    public class UserPanelSideBarViewModel
    {
        public string? UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string? PitcureName { get; set; }
    }

    public class EditProfileViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Email { get; set; }

        public IFormFile? UserAvatar { get; set; }
    }
}
