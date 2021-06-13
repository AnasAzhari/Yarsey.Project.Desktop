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
    public class SaleDataService : ISaleService
    {
        private readonly YarseyDbContextFactory _yarseyDbContextFactory;
        private readonly NonQueryDataService<Sale> _nonQueryDataService;

        public SaleDataService(YarseyDbContextFactory contextFactory)
        {
            this._yarseyDbContextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Sale>(contextFactory);
        }

        public async Task<Sale> Create(Sale entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Sale> Get(int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Sale entity = await dbContext.Sales
                                        .FirstOrDefaultAsync((a) => a.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Sale>> GetAll()
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                IEnumerable<Sale> sales = await dbContext.Sales
                                                        .ToListAsync();
                return sales;
            }
        }

        public async Task<Sale> GetSaleById(int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Sale entity = await dbContext.Sales
                                        .FirstOrDefaultAsync((a) => a.Id == id);
                return entity;
            }
        }

        public async Task<Sale> GetSaleByIdInBusiness(int businessId, int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business biz = await dbContext.Businesses
                                        .FirstOrDefaultAsync((a) => a.Id == businessId);
                Sale sale = biz.Sales.FirstOrDefault((b) => b.Id == id);
                return sale;
            }
        }

        public Task<Sale> GetSaleByRunningNo(string runningNo)
        {
            throw new NotImplementedException();
        }

        public Task<Sale> GetSaleByRunninNoInBusiness(int businessId, string runningNo)
        {
            throw new NotImplementedException();
        }

        public async Task<Sale> Update(int id, Sale entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
