using System;
using System.Threading.Tasks;
using AirlineSendAgent.App;
using AirlineSendAgent.Client;
using AirlineSendAgent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AirlineSendAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IAppHost, AppHost>();
                    services.AddSingleton<IWebhookClient, WebhookClient>();
                    services.AddSingleton<IWebhookSecretHost, WebhookSecretHost>();
                    services.AddDbContext<SendAgentDbContext>(opt =>
                        opt.UseSqlServer("Server=localhost,1433;Database=AirlineWebDB;User Id=sa;Password=pa55w0rd!"));
                    services.AddHttpClient();
                }).Build();
            
            // host.Services.GetService<IWebhookSecretHost>().Run();
            // host.Services.GetService<IAppHost>().Run();
            void T1() => host.Services.GetService<IAppHost>().Run();
            void T2() => host.Services.GetService<IWebhookSecretHost>().Run();
            Parallel.Invoke((Action) T1, T2);
        }
    }
}
