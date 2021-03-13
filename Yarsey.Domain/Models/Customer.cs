using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class Customer:DomainObject
    {

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        [MaxLength(250)]
        public string Adress { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(14)]
        public string PhoneNo { get; set; }
        
        [MaxLength(250)]
        public string Notes { get; set; }

        public DateTime? Logged { get; set; }

        public DateTime Created_at { get; set; }
    }
}
