using Infrastructure.Context;
using Infrastructure.Entities;
using Infrastructure.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SalesRepository : Repository<Sale>, ISalesRepository
    {
        private readonly MarketingDbContext _db;

        public SalesRepository(MarketingDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
