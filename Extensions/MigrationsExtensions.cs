using CardShop.Context;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CardShop.Extensions
{
    public static class MigrationsExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using CardShopDbContext dbContext = scope.ServiceProvider.GetRequiredService<CardShopDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
