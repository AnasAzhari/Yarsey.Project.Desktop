using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;
using Yarsey.Domain.Services;
using Yarsey.EntityFramework.Services.Common;

namespace Yarsey.EntityFramework.Services
{
    public class InvoiceDataService : IInvoiceService
    {
        private readonly YarseyDbContextFactory _yarseyDbContextFactory;
        private readonly NonQueryDataService<Invoice> _nonQueryDataService;
        public InvoiceDataService(YarseyDbContextFactory contextFactory)
        {
            this._yarseyDbContextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Invoice>(contextFactory);
        }
        public async Task<Invoice> Create(Invoice entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Invoice> Get(int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Invoice entity = await dbContext.Invoices.FirstOrDefaultAsync((a) => a.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Invoice>> GetAll()
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                IEnumerable<Invoice> entity = await dbContext.Invoices.ToListAsync();
                return entity;
            }
        }
        public async Task<IEnumerable<Invoice>> GetAllBusinessInvoice(Business business)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business entity = await dbContext.Businesses.Include(i => i.Invoices).FirstOrDefaultAsync(b => b.Id == business.Id);
                IEnumerable<Invoice> invoices = entity.Invoices;
                return invoices;
            }
        }

        public async Task<Invoice> GetBusinessInvoiceById(Business business, int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business entity = await dbContext.Businesses.Include(i => i.Invoices).FirstOrDefaultAsync(b => b.Id == business.Id);
                Invoice invoice = entity.Invoices.FirstOrDefault(x=>x.Id==id);
                return invoice;
            }
        }

        public async Task<Invoice> GetInvoiceById(int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Invoice invoice = await dbContext.Invoices.Include(x => x.ProductsSelected).FirstOrDefaultAsync(i=>i.Id==id);
                return invoice;
            }
        }

        public async Task<Invoice> Update(int id, Invoice entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public int GetCurrentNo(string module)
        {

            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                var v = dbContext.RunningNumbers.Where(x => x.ModuleName == module).Select(h => h.RunningNo).FirstOrDefault();

                return v;
            }

        }

        public string GetCurrentRunningNo(string module)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                var v = dbContext.RunningNumbers.Where(x => x.ModuleName == module).Select(h => h.RunningNo).FirstOrDefault();
                char pad = '0';
                string numberString = v.ToString().PadLeft(8, pad);
                string fullString = $"{module}{numberString}";
                return fullString;
            }
        }

        public string GetNextRunningNo(string module)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                var v = dbContext.RunningNumbers.Where(x => x.ModuleName == module).Select(h => h.RunningNo).FirstOrDefault();
                char pad = '0';
                string numberString = (v + 1).ToString().PadLeft(8, pad);
                string fullString = $"{module}{numberString}";
                return fullString;
            }
        }

  
    }
}
