using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Users.ViewModels
{
    public class ClassifiedOwnerModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string GSM { get; set; }
        public string Email { get; set; }
    }
}
