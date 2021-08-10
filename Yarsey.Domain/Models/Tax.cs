using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public enum TaxType
    {
        Type1,
        Type2
    }

    public class Tax
    {
        public TaxType TaxType { get; set; }
        public decimal TaxValue { get; set; }

    }
}
