using Data;
using Data.Domain;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Interface;

namespace Shared.Service
{
    public class ProductService : IProductService
    {
        public readonly VisitContext _orderContext;
        public ProductService(VisitContext orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<List<Product>> GetAsync(string searchString)
        {
            return await _orderContext.Product.Where(p => p.Name.Contains(searchString)).ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            var product = await _orderContext.Product.SingleOrDefaultAsync(p => p.Id == id);
            if (product is null) { product = new Product(); }
            return product;
        }
    }
}