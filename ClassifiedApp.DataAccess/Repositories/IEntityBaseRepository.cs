using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Repositories
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> All { get; }
        IEnumerable<T> GetAll();
        T GetById(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindSingleBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        void Edit(T entity);
    }
}
