using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Services;
using Yarsey.EntityFramework.Services;

namespace Yarsey.Desktop.WPF.UnitOfWork
{
    interface IUnitOfWork
    {
        IBusinessService businessDataService {get;}
        ICustomerService customerService {get;}
        IInvoiceService invoiceService {get;}
        Task<bool> Complete();
        bool HasChanges();

    }
}
