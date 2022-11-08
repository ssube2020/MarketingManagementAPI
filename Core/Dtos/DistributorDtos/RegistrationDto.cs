using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class RegistrationDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "maximum {1} characters allowed")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "maximum {1} characters allowed")]
        public string Surname { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "maximum {1} characters allowed")]
        public string Gender { get; set; }
        public string? Imageurl { get; set; } 
        public IFormFile? Image { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "maximum {1} characters allowed")]
        public string PrivateDocType { get; set; }
        [MaxLength(10, ErrorMessage = "maximum {1} characters allowed")]
        public string? DocSerialNumber { get; set; }
        [MaxLength(10, ErrorMessage = "maximum {1} characters allowed")]
        public string? DocNumber { get; set; }

        [Required]
        public DateTime GiveOutDate { get; set; }

        [Required]
        public DateTime DocExpDate { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "PN must be max {1} characters")]
        public string PrivateNumber { get; set; }
        [MaxLength(50, ErrorMessage = "giving authority can be maximum {1} characters allowed")]
        public string? GivingAuthority { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "contact type can be maximum {1} characters allowed")]
        public string ContactType { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "contact info maximum {1} characters allowed")]
        public string ContactInfo { get; set; }
        [MaxLength(20, ErrorMessage = "maximum {1} characters allowed")]
        public string? AddressType { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "address can be maximum {1} characters allowed")]
        public string Address { get; set; }
        public int? RecomendatorId { get; set; }
    }
}
