using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{


    public  class Transaction:DomainObject
    {

        public enum TransactionTypes
        {
            In,
            Out

        }

        public int Sales_id { get; set; }
        public TransactionTypes TransactionType { get; set; }

        public double Amount { get; set; }

    }
}
