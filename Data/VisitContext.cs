using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Data.Domain.Extensions;
using Data.Domain;

namespace Data
{
    public class VisitContext : DbContext
    {
        public VisitContext(DbContextOptions options) : base(options)
        {
            // Set API as Startup Project
            // Set Default project in Package Manager Console to Data
            // add-migration Initial -context VisitContext -o Migrations
        }
        public DbSet<Visit> Visit { get; set; } = default!;
        public DbSet<Service> Service { get; set; } = default!;
        public DbSet<Address> Address { get; set; } = default!;
        public DbSet<Customer> Customer { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties());
            foreach (var property in properties
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                property.SetColumnType("decimal(18, 6)");
            foreach (var property in properties
                .Where(p => p.ClrType == typeof(string)))
                property.SetColumnType("nvarchar(255)");
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var time = DateTimeExtensions.Now();
            foreach (var entityEntry in ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified)))
            {
                var entity = (BaseEntity)entityEntry.Entity;
                entity.DateModified = time;
                entity.UpdatedBy = "SYSTEM";

                if (entityEntry.State != EntityState.Added)
                {
                    continue;
                }

                entity.DateCreated ??= time;
                entity.CreatedBy = "SYSTEM";
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Orders;Integrated Security=True;");
            optionsBuilder.UseExceptionProcessor();
        }
    }
}