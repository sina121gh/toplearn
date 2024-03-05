using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Generator;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.Services
{
    public class FileService : IFileService
    {
        public void DeleteAvatar(string avatarName)
        {
            if (avatarName != "DefaultAvatar.png")
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "UsersAvatars",
                    avatarName);
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
        }

        public string SaveAvatar(IFormFile avatar)
        {
            string avatarName = MyGenerator.GenerateCode() + Path.GetExtension(avatar.FileName);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "UsersAvatars",
                    avatarName);
            using (FileStream stream = new FileStream(imagePath, FileMode.Create))
            {
                avatar.CopyTo(stream);
            }

            return avatarName;
        }
    }
}
