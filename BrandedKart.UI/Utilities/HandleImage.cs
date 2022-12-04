using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BrandedKart.UI.CommonTasks
{
    public static class HandleImage
    {
        public static string userImageFolder = "ProfileImages";
        public static string productImageFolder = "ProductImages";
        public static string SaveImage<TEntity>(HttpPostedFileBase Image, TEntity Obj, string saveImageFolder, HttpServerUtilityBase Server)
        {
            string fileExtension, path;
            Random randomNum = new Random();
            int randomNumber = randomNum.Next();

            fileExtension = Path.GetExtension(Image.FileName);

            if (fileExtension.ToLower().Equals(".jpg") || fileExtension.ToLower().Equals(".png") || fileExtension.ToLower().Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                StringBuilder dbImagePath = new StringBuilder(randomNumber + Path.GetFileName(Image.FileName));
                path = Path.Combine(Server.MapPath("~/Content/Images/" + saveImageFolder), dbImagePath.ToString());
                Image.SaveAs(path);
                dbImagePath.Insert(0, "~/Content/Images/" + saveImageFolder + "/");
                return dbImagePath.ToString();
            }

            return null;
        }
    }
}