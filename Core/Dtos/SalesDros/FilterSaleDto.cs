using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class FilterSaleDto
    {
        public int? DistributorId { get; set; }
        public DateTime? SaleDate { get; set; }
        public string? ProductCode { get; set; }
    }
}
