using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class Customer:DomainObject
    {
        
        public string Name { get; set; }
        public string PhoneNo { get; set; }
    }
}
