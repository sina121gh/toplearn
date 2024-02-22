using System;
using System.Collections.Generic;
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
}
