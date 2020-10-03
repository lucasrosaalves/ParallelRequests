using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ParallelRequests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RegisterServices().GetService<IClient>().Handle();
        }

        public static IServiceProvider RegisterServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IClient, Client>();

            return services.BuildServiceProvider();
        }
    }

}
