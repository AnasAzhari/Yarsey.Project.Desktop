using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class ProductPurchaseDetail:DomainObject
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public string PurchaseDescription { get; set; }
        public decimal PurchasePrice { get; set; }
        //public Tax PurchaseTax { get; set; }
    }
}
