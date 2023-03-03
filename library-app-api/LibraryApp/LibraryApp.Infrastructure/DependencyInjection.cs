using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data;
using LibraryApp.Infrastructure.Data.Repositories;
using LibraryApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Infrastructure;
public static class DependencyInjection
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<LibraryAppContext>(options =>
            options.UseSqlite(configuration.GetConnectionString(Constants.ConnectionStringName)));

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRandomPatronGenerator, RandomPatronGenerator>();

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IBooksRepository, BooksRepository>();
        services.AddScoped<IPatronsRepository, PatronsRepository>();
        services.AddScoped<IBookInstancesRepository, BookInstancesRepository>();
    }
}
