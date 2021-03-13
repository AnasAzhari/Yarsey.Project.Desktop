using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Yarsey.EntityFramework
{
    public class YarseyDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public YarseyDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public YarseyDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<YarseyDbContext> options = new DbContextOptionsBuilder<YarseyDbContext>();
            _configureDbContext(options);
            return new YarseyDbContext(options.Options);
        }
    }
}
