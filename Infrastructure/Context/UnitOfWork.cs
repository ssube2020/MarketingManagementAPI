using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MarketingDbContext _db;

        public UnitOfWork(MarketingDbContext db)
        {
            _db = db;
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
