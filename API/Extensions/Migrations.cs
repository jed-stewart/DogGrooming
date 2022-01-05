using Shared.Interface;
namespace API.Extensions
{
    public static class Migrations
    {
        public static async Task MigrateDatabasesAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var databaseService = serviceProvider.GetRequiredService<IDatabaseService>();
            await databaseService.Migrate();
        }
    }
}
