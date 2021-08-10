using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarsey.Domain.Models;
using Yarsey.Domain.Services;

namespace Yarsey.EntityFramework.Services
{
    public class AccountDataService : IAccountService
    {
        private readonly YarseyDbContextFactory _yarseyDbContextFactory;

        public AccountDataService(YarseyDbContextFactory contextFactory)
        {
            this._yarseyDbContextFactory = contextFactory;
        }
        public Task<IEnumerable<Account>> GetBusinessAccounts(int bizId)
        {
            throw new NotImplementedException();
        }
        public async Task GenerateDefaultAccounts(int bizId)
        {
            using (YarseyDbContext dbContext = _yarseyDbContextFactory.CreateDbContext())
            {
                Business entity = await dbContext.Businesses.Include(c => c.Accounts)
                                        .FirstOrDefaultAsync(b=>b.Id==bizId);
                if (!entity.Accounts.Any())
                {
                    entity.Accounts = ListOfDefaultAccounts();
                    await dbContext.SaveChangesAsync();
                }
                return;
            }
        }


        public List<Account> ListOfDefaultAccounts()
        {
            // create default Account 

            List<Account> accounts = new List<Account>();

            // asset - 1000

            Account accountReceivable = new Account()
            {
                AccountType = AccountType.Assets,
                Code = "1001",
                Name = "Account Receivable"

            };
            accounts.Add(accountReceivable);

            Account accountcash = new Account()
            {
                AccountType = AccountType.Assets,
                Code = "1002",
                Name = "Cash At Bank"
            };
            accounts.Add(accountcash);


            // liability - 2000

            Account accountPayable = new Account()
            {
                AccountType = AccountType.Liabilities,
                Code = "2001",
                Name = "Accounts Payable"
            };
            accounts.Add(accountPayable);


            // expenses - 3000

            Account accountGeneral = new Account()
            {
                AccountType = AccountType.Expenses,
                Code = "3001",
                Name = "General Expense"
            };
            accounts.Add(accountGeneral);

            Account accountElectricity = new Account()
            {
                AccountType = AccountType.Expenses,
                Code = "3002",
                Name = "Electricty"
            };
            accounts.Add(accountElectricity);

            Account accountWater = new Account()
            {
                AccountType = AccountType.Expenses,
                Code = "3003",
                Name = "Water"
            };
            accounts.Add(accountWater);

            Account accountRent = new Account()
            {
                AccountType = AccountType.Expenses,
                Code = "3004",
                Name = "Rent Expense"
            };
            accounts.Add(accountRent);

            Account accountOffice = new Account()
            {
                AccountType = AccountType.Expenses,
                Code = "3005",
                Name = "Office Expense"
            };
            accounts.Add(accountOffice);

            Account accountTelephoneAndInternet = new Account()
            {
                AccountType = AccountType.Expenses,
                Code = "3006",
                Name = "Telephone & Internet"
            };
            accounts.Add(accountTelephoneAndInternet);

            Account accountLegal = new Account()
            {
                AccountType = AccountType.Expenses,
                Code = "3007",
                Name = "Legal Fees"
            };
            accounts.Add(accountLegal);

            // equity - 4000
            Account accountRetainedEarning = new Account()
            {
                AccountType = AccountType.Equity,
                Code = "4001",
                Name = "Retained Earning"
            };
            accounts.Add(accountRetainedEarning);

            // income - 5000

            Account accountInterestEarned = new Account()
            {
                AccountType = AccountType.Income,
                Code = "5001",
                Name = "Interest Earned"
            };
            accounts.Add(accountInterestEarned);


            Account accountInventorySales = new Account()
            {
                AccountType = AccountType.Income,
                Code = "5002",
                Name = "Inventory Sales"
            };
            accounts.Add(accountInventorySales);

            Account accountSales = new Account()
            {
                AccountType = AccountType.Income,
                Code = "5003",
                Name = "Sales"
            };
            accounts.Add(accountSales);


            return accounts;
        }

    }
}
