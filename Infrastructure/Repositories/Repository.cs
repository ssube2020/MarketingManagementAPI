using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IPrimaryKey
    {
        private readonly MarketingDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(MarketingDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return _db.Set<T>().AsQueryable();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<T> InsertAndReturnAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Get(int id)
        {
            var toReturn = _dbSet.Where(x => x.Id == id).SingleOrDefault();
            if(toReturn != null)
            {
                return toReturn;
            }
            return null;
        }

        public void Remove(int id)
        {
            _dbSet.Remove(Get(id));
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
