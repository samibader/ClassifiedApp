﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.Models
{
    public class FeedbackTicket : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Note { get; set; }
        public virtual User User { get; set; }
        public DateTime IssuedDate { get; set; }
    }
}
