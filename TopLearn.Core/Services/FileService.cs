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

        public void DeleteDemo(string demoName)
        {
            string demoPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "courses",
                    "demos",
                    demoName);
            if (File.Exists(demoPath))
                File.Delete(demoPath);
        }

        public void DeleteImage(string imageName)
        {
            if (imageName != "DefaultCourseImage.png")
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "courses",
                    "images",
                    imageName);
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

        public string SaveDemo(IFormFile demo)
        {
            string demoName = MyGenerator.GenerateCode() + Path.GetExtension(demo.FileName);
            string demoPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "courses",
                    "demos",
                    demoName);
            using (FileStream stream = new FileStream(demoPath, FileMode.Create))
            {
                demo.CopyTo(stream);
            }

            return demoName;
        }

        public string SaveImage(IFormFile image)
        {
            string imageName = MyGenerator.GenerateCode() + Path.GetExtension(image.FileName);
            string imagePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "courses",
                    "images",
                    imageName);
            using (FileStream stream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return imageName;
        }
    }
}
