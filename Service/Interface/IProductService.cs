using Data.Domain;

namespace Shared.Interface
{
    public interface IProductService
    {
        Task<Product> GetAsync(int id);
        Task<List<Product>> GetAsync(string searchString);
    }
}