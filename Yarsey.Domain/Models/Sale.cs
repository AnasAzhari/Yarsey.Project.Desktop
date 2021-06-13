using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
  
    public class Sale:DomainObject
    {
        public enum SalesCategories
        {
            Categorized,
            Uncategorized
        }


        public SalesCategories SaleCategory { get; set; }
        public Transaction Transaction { get; set; }
        public int Customer_id { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [MaxLength(250)]
        public string Notes { get; set; }
    }
}
