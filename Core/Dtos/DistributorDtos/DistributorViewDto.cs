using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class DistributorViewDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string PrivateDocType { get; set; }
        public string? DocSerialNumber { get; set; }
        public string? DocNumber { get; set; }
        public DateTime GiveOutDate { get; set; }
        public DateTime DocExpDate { get; set; }
        public string PrivateNumber { get; set; }
        public string? GivingAuthority { get; set; }
        public string ContactType { get; set; }
        public string ContactInfo { get; set; }
        public string? AddressType { get; set; }
        public string Address { get; set; }
        public string? Recomendator { get; set; }

    }
}
