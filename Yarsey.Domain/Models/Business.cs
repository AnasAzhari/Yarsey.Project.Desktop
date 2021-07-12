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
        [MaxLength(128)]
        public string BusinessName { get; set; }

        [MaxLength(128)]
        public string RegistrationNo { get; set; }

        [Required]
        [MaxLength(128)]
        public string PhoneNo { get;set; }

        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(128)]
        public string Adresss { get; set; }

        public byte[] Image { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

    }
}
