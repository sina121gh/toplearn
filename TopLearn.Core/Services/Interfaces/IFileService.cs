using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IFileService
    {
        #region User Avatar

        string SaveAvatar(IFormFile avatar);
        void DeleteAvatar(string avatarName);

        #endregion

        #region Course

        #region Image

        string SaveImage(IFormFile image);
        void DeleteImage(string imageName);

        #endregion

        #region Demo

        string SaveDemo(IFormFile demo);
        void DeleteDemo(string demoName);

        #endregion

        #endregion

    }
}
