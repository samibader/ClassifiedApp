using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ClassifiedAppDbContext Init();
    }
}
