using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class Business: DomainObject
    {

        [Required]
        public string BusinessName { get; set; }

        public string RegistrationNo { get; set; }

        [Required]
        public string PhoneNo { get;set; }


        public string Adresss { get; set; }

        public byte[] Image { get; set; }

    }
}
