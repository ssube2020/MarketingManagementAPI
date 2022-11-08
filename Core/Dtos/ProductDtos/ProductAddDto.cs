using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class ProductAddDto
    {
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0,double.MaxValue, ErrorMessage = "price must be more or equal than 0")]
        public double UnitPrice { get; set; }
    }
}
