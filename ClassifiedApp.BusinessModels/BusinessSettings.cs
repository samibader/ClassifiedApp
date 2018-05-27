using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessModels
{
    public static class BusinessSettings
    {
        public static readonly DateTime ServerNow = DateTime.Now;
        public static readonly DateTime AuthTokenExpiry = DateTime.MaxValue;
        
    }
}
