using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ClassifiedAppDbContext dbContext;

        public ClassifiedAppDbContext Init()
        {
            return dbContext ?? (dbContext = new ClassifiedAppDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
