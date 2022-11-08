using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class getBonusesDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public double? MinBonus { get; set; }
        public double? MaxBonus { get; set; }
    }
}
