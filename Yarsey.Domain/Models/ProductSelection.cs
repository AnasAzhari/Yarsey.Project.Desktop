using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public decimal PricePerItem { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }

        public string Word { get; set; } 

   

    }
}
