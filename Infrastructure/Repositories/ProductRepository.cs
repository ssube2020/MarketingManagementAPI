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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IRepository<Distributor> _repository;
        private readonly MarketingDbContext _db;

        public ProductRepository(IRepository<Distributor> repository, MarketingDbContext db) : base(db)
        {
            _repository = repository;
            _db = db;
        }
    }
}
