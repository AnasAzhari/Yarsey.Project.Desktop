using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Desktop.WPF.Services
{
    public interface INavigationService
    {
        public void Navigate();

        public void NavigateEditCustomer(Customer customer);
    }
}
