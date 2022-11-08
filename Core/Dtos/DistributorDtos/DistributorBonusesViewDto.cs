using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class DistributorBonusesViewDto
    {
        public List<DistributorBonusViewDto> Bonuses { get; set; }
        public string? Message { get; set; }
    }
}
