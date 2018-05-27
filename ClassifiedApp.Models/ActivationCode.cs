using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class ActivationCode : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Reason { get; set; }
        public string Code { get; set; }
        public System.DateTime IssuedOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
        public virtual User User { get; set; }
    }
}
