using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yarsey.Domain.Models
{

    public class RunningNumber:DomainObject
    {
        public string ModuleName { get; set; }

        public string Prefix { get; set; }
        public int RunningNo { get; set; }
    }
}
