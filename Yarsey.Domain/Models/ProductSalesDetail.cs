using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class ProductSalesDetail:DomainObject
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string SalesDescription { get; set; }
        public decimal SalesPrice { get; set; }
        //public Tax SalesTax { get; set; }

    }
}

