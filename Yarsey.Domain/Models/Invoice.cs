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
        public int Customer_Id { get; set; }
        public string ref_no { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime InvoiceDate { get; set; }
        public DateTime Due { get; set; }
        public string Adress { get; set; }
        public ICollection<ProductSelection> ProductsSelected { get; set; }


    }
}
