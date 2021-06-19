using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class Invoice:DomainObject
    {

        public Customer Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime Due { get; set; }
        public ICollection<ProductSelection> ProductsSelected { get; set; }
        public bool Taxed { get; set; } = false;
        public int Taxedpercentage { get; set; }




    }
}
