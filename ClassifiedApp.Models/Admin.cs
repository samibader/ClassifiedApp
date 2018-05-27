using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class Admin : IEntityBase
    {
        public Admin()
        {

        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }
}
