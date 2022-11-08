using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Sale : BaseEntity
    {
        public int DistributorId { get; set; }
        public DateTime SaleDate { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
