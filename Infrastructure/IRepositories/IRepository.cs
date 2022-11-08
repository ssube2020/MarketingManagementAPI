using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositories
{
    public interface IRepository<T> where T : class, IPrimaryKey
    {
        IQueryable<T> Query();
        void Add(T entity);
        Task<T> InsertAndReturnAsync(T entity);
        void Update(T entity);
        T Get(int id);
        List<T> GetAll();
        void Remove(int id);
    }
}
