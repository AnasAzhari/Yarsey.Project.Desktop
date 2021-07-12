using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;

namespace Yarsey.Domain.Services
{
    public interface IInvoiceService:IDataService<Invoice>
    {
        Task<Invoice> GetInvoiceById(int id);
        Task<Invoice> GetBusinessInvoiceById(Business business,int id);
        Task<IEnumerable<Invoice>> GetAllBusinessInvoice(Business business);

    }
}
