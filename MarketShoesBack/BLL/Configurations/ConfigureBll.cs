using BLL.Services;
using DLL.Context;
using DLL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Configurations
{
    public class ConfigureBll
    {
        public static void Configere(IServiceCollection collection,string connectionString)
        {
            collection.AddDbContext<MarketShoesContext>(o => o.UseSqlServer(connectionString));
            collection.AddTransient<UserRepository>();
            collection.AddTransient<AuthorizeService>();
            
        }

    }
}
