using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class Rate:IEntityBase
    {
        public Rate()
        {

        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ClassifiedId { get; set; }
        public virtual Classified Classified { get; set; }
        public DateTime CreationDate { get; set; }
        public byte Value { get; set; }

    }
}
