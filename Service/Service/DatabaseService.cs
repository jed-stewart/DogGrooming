using Data;
using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Shared.Interface;
using Bogus;

namespace Shared.Service
{
    public class DatabaseService : IDatabaseService
    {
        public readonly OrderContext _orderContext;
        public readonly Faker _faker;
        public DatabaseService(OrderContext orderContext)
        {
            _orderContext = orderContext;
            _faker = new Faker
            {
                Random = new Randomizer(8675309)
            };
        }

        public async Task MigrateAsync()
        {
            await _orderContext.Database.MigrateAsync();
            await SeedProductsAsync(100);
        }

        public async Task SeedProductsAsync(int count)
        {
            if (await _orderContext.Product.AnyAsync())
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
                _orderContext.Product.Add(product);

                await _orderContext.SaveChangesAsync();
            }
        }
    }
}