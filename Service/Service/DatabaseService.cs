using Data;
using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Interface;
using Bogus;

namespace Shared.Service
{
    public class DatabaseService : IDatabaseService
    {
        public readonly VisitContext _visitContext;
        public readonly Faker _faker;
        public DatabaseService(VisitContext visitContext)
        {
            _visitContext = visitContext;
            _faker = new Faker
            {
                Random = new Randomizer(8675309)
            };
        }

        public async Task MigrateAsync()
        {
            await _visitContext.Database.MigrateAsync();
            await SeedProductsAsync(100);
        }

        public async Task SeedProductsAsync(int count)
        {
            if (await _visitContext.Product.AnyAsync())
            {
                return;
            }

            for (var i = 0; i < count; i++)
            {
                decimal price;
                Decimal.TryParse(_faker.Commerce.Price(), out price);
                var product = new Product
                {
                    Name = _faker.Commerce.Product(),
                    Price = price,
                    Description = _faker.Commerce.ProductDescription(),

                };
                _visitContext.Product.Add(product);

                await _visitContext.SaveChangesAsync();
            }
        }
    }
}