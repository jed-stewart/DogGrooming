using Data.Domain;

namespace Shared.Interface
{
    public interface IProductService
    {
        Task<Product> Get(int id);
        Task<List<Product>> Get(string searchString);
    }
}