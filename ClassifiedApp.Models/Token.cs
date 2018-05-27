using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class Token : IEntityBase
    {
        public Token()
        {

        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid AuthToken { get; set; }
        public System.DateTime IssuedOn { get; set; }
        //public System.DateTime ExpiresOn { get; set; }
        public virtual User User { get; set; }
    }
}
