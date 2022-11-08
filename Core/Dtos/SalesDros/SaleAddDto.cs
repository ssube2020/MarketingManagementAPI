using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class SaleAddDto
    {
        [Required]
        public int DistributorId { get; set; }
        [Required]
        public DateTime SaleDate { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
    }
}
