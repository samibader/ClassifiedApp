using ClassifiedApp.DataAccess.Infrastructure;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
            where T : class, IEntityBase, new()
    {

        private ClassifiedAppDbContext dataContext;

        #region Properties
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ClassifiedAppDbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        public EntityBaseRepository(ClassifiedAppDbContext _context)
        {
            this.dataContext = _context;
        }
        #endregion
        public virtual IEnumerable<T> GetAll()
        {
            return DbContext.Set<T>().ToList();
        }
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate).ToList();
        }
        public virtual IEnumerable<T> All
        {
            get
            {
                return GetAll();
            }
        }
        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
        public T GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate).AsQueryable<T>();
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            DbContext.Set<T>().Add(entity);
        }
        public virtual void Edit(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> objects = FindBy(predicate);
            foreach (T obj in objects)
                Delete(obj);
        }

        public T FindSingleBy(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate).FirstOrDefault();
        }
    }
}
