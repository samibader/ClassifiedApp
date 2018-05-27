using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassifiedApp.BusinessServices.Users.ViewModels
{
    public class ActivationCodeModel
    {
        //public int Id { get; set; }
        public int UserId { get; set; }
        public string Reason { get; set; }
        public string Code { get; set; }
        public System.DateTime IssuedOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
        public UserModel User { get; set; }
    }
}
