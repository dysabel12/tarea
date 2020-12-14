using api_exchange_rate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_exchange_rate.Database
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExchangeRateDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<ExchangeRateDBContext>>()))
            {
                if (context.ExchangeRates.Any())
                {
                    return;
                }

                context.ExchangeRates.AddRange(
                    new ExchangeRate
                    {
                        id = 1,
                        currency_input = 1,
                        currency_output = 2,
                        exchange_rate = 0.3M
                    });

                if (context.Currencies.Any())
                {
                    return;
                }

                context.Currencies.AddRange(
                    new Currency
                    {
                        id = 1,
                        name = "PEN S/. - Peruvian Nuevo Sol"
                    },
                    new Currency
                    {
                        id = 2,
                        name = "USD $ - US Dollar"
                    });

                context.SaveChanges();
            }
        }
    }
}
