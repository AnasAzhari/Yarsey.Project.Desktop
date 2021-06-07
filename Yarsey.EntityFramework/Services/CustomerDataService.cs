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
    public class CustomerDataService : ICustomerService
    {
        private readonly YarseyDbContextFactory _yarseyDbContextFactory;

        private readonly NonQueryDataService<Customer> _nonQueryDataService;

        public CustomerDataService(YarseyDbContextFactory contextFactory)
        {
            _yarseyDbContextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Customer>(contextFactory);
        }


        public async Task<Customer> Create(Customer entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Customer> Get(int id)
        {
            using (YarseyDbContext dbContext =_yarseyDbContextFactory.CreateDbContext())
            {
                Customer entity = await dbContext.Customers
                                        .FirstOrDefaultAsync((a) => a.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                IEnumerable<Customer> customers = await dbContext.Customers
                                                        .ToListAsync();
                return customers;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersByBusineness(int id)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business biz = await dbContext.Businesses.FirstAsync(x => x.Id == id);
                var customers = biz.Customers.ToList();
                                                        ;
                return customers;
            }
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Customer customer = await dbContext.Customers
                                          .FirstOrDefaultAsync((a)=>a.Name==name);
                return customer;
            }
        }

        public async Task<Customer> Update(int id, Customer entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
