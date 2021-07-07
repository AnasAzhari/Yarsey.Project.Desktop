using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class ProductSelection:DomainObject
    {
        public Product SelectedProduct { get; set; }
        public int SelectedProductId { get; set; }
        public int Quantity { get; set; }

        public Decimal PricePerItem { get; set; }
        public decimal Discount { get; set; } 

    }
}
