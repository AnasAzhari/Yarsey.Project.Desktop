using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{
    public class Supplier:DomainObject
    {
        public string Name { get; set; }

        public string BusinessIdentifierNo { get; set; }

        public string Email { get; set; }

        public string Adress { get; set; }

    }
}
