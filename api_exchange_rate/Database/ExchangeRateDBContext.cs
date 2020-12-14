using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_exchange_rate.Database
{
    public class ExchangeRateDBContext : DbContext
    {
        public ExchangeRateDBContext(DbContextOptions<ExchangeRateDBContext> options)
           : base(options)
        {
        }

        public DbSet<Models.ExchangeRate> ExchangeRates { get; set; }

        public DbSet<Models.Currency> Currencies { get; set; }
    }
}
