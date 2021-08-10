using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Services;
using Yarsey.EntityFramework;

namespace Yarsey.Desktop.WPF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly YarseyDbContext _context;

        public UnitOfWork(YarseyDbContext context)
        {
            this._context = context;
        }


        public IBusinessService businessDataService => throw new NotImplementedException();

        public ICustomerService customerService => throw new NotImplementedException();

        public IInvoiceService invoiceService => throw new NotImplementedException();

        public IAccountService accountService => throw new NotImplementedException();

        public async Task<bool> Complete()
        {
           return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
