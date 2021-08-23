using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Domain.Services
{
    public  interface IBusinessService:IDataService<Business>
    {
        Task<Business> GetBusinessByName(string name);
        Task<Business> GetDefault();
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Customer customer);
        Task AddCustomer(int bizId, Customer customer);
        Task AddProduct(int bizId, Product product);
        Task AddInvoice(int bizId, Invoice invoice, string module);
        Task DeleteProduct(Product product);

        Task GenerateDefaultRunningNumbers(int bizId);
    }
}
