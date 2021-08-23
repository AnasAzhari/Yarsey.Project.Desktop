using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public enum AccountType
    {
        Assets,
        Liabilities,
        Equity,
        Income,
        Expenses
    }

    public class Account:DomainObject
    {
        public AccountType AccountType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        
    }
}
