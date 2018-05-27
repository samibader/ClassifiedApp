using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices
{
    public static class BusinessSettings
    {
        public static DateTime ServerNow { get { return DateTime.Now; } }
        public static readonly int DaysBetweenClassifieds = 30;

        public static readonly DateTime AuthTokenExpiry = DateTime.MaxValue;

        public static readonly String BaseUrl = "http://classifiedapp.gear.host";
        //public static readonly String BaseUrl = "http://localhost:50975";
        public static readonly String CategoryImageFolderBase = "images/categories";
        public static readonly String SubCategoryImageFolderBase = "images/subcategories";
        public static readonly String ClassifiedImageFolderBase = "images/classifieds";
        public static readonly String ClassifiedImagePlaceHolder = string.Join("/", BusinessSettings.BaseUrl, BusinessSettings.ClassifiedImageFolderBase, "placeholder.jpg");
        public static readonly String SyriatelUserName = "Classified_N";
        public static readonly String SyriatelPassword = "P@123456";
        public static readonly String SyriatelSender = "Classified";
        public static readonly String SyriatelJobName = "TestMsg";

        public static readonly String SmtpUserName = "S.Bader@infosyria.com";
        public static readonly String SmtpPassword = "123qwe";
        public static readonly String SmtpHost = "mail.infosyria.com";
        public static readonly Int32 SmtpPort = 587;

    }
}
