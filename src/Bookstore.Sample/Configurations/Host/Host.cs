using AHSW.EventLog.Extensions;
using Bookstore.Sample.DatabaseContext;
using Bookstore.Sample.Interfaces;
using Bookstore.Sample.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bookstore.Sample.Configurations;

internal static class Host
{
    public static async Task<IHost> CreateAsync(bool recreateDatabase, Func<BookstoreDbContext, Task> configureEventLog)
    {
        var host = CreateHostBuilder().Build();
        
        using var scope = host.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
        
        ApplyPendingMigrations(context, recreateDatabase);

        await configureEventLog(context);
        
        return host;
    }

    private static IHostBuilder CreateHostBuilder()
    {
        var hostBuilder = new HostBuilder();

        hostBuilder.ConfigureServices((_, services) =>
        {
            services.AddDbContext<BookstoreDbContext>(options =>
            {
                options.UseSqlite("Data Source=Bookstore.db");
            });

            services.AddEventLog<BookstoreDbContext, EventType, EntityType, PropertyType>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IShelfRepository, ShelfRepository>();
        });
        
        return hostBuilder;
    }
    
    private static void ApplyPendingMigrations(BookstoreDbContext context, bool recreateDatabase)
    {
        if (recreateDatabase)
        {
            context.Database.EnsureDeleted();
            Console.WriteLine("The Bookstore database is deleted.");
        }
        
        var pendingMigrationNames = context.Database.GetPendingMigrations().ToList();
        
        if (!pendingMigrationNames.Any())
        {
            Console.WriteLine("The Bookstore database is up to date.");
            return;
        }
        
        Console.WriteLine("Pending migrations: {0}", string.Join(", ", pendingMigrationNames));
        
        context.Database.Migrate();

        Console.WriteLine("The Bookstore database was successfully updated.");
    }
}