using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Users.ViewModels
{
    public class UserModel 
    {
        
        public int Id { get; set; }
        public string Username { get; set; }
        //public string HashedPassword { get; set; }
        //public string Salt { get; set; }
        public string GSM { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool IsEmailApproved { get; set; }
        public bool IsGsmApproved { get; set; }
        public bool IsBlocked { get; set; }
        public Guid UserGuid { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsGpsEnabled { get; set; }
        //public List<TokenModel> Tokens { get; set; }
    }
}
