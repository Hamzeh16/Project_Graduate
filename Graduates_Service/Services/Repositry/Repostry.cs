using Graduates_Data.Data;
using Graduates_Service.Services.Repositry.IRepositry;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Graduates_Service.Services.Repositry
{
    public class Repostry<T> : IRepositray<T> where T : class
    {

        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        public Repostry(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filtter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
