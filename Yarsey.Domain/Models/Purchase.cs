using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class Purchase:DomainObject
    {
        public Transaction Transaction { get; set; }
    }
}
