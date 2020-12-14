using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api_exchange_rate.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace api_exchange_rate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //1. obtenemos IWebHost el cual aloja la aplicacion
            var host = CreateWebHostBuilder(args).Build();

            //2. Buscamos la capa dentro de nuestro scope
            using (var scope = host.Services.CreateScope())
            {
                //3. obtenemos la instancia del dbcontext
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ExchangeRateDBContext>();

                //4. Creamos data
                DataGenerator.Initialize(services);
            }

            //Corre la aplicación
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
