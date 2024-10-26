using Graduates_Data.Data;
using Graduates_Services.Services.IRepostry;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Graduates_Services.Services.Repostry
{
    public class REPOSITRY : IREPOSITRY<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        public REPOSITRY(AppDbContext db)
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
