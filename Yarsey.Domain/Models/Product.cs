using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public enum ProductUom
    {
        Kg,
        Unit,
        Kotak,
        Tan
    }
    public class Product:DomainObject
    {

        public string ProductName { get; set; }
      
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public ProductUom ProductUOM { get; set; }

        public ProductSalesDetail ProductSalesDetail { get; set; }
        public ProductPurchaseDetail ProductPurchaseDetail { get; set; }


        public string Notes { get; set; }
    }
}
