﻿using System.Linq.Expressions;

namespace Graduates_Services.Services.IRepostry
{
    public interface IREPOSITRY<T> where T : class
    {
        // T - Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filtter);
        void Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
    }
}
