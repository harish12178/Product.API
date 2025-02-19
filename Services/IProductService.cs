using Zeiss.ProductApi.Models;

namespace Zeiss.ProductApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task DecrementStockAsync(int id, int quantity);
        Task AddToStockAsync(int id, int quantity);
    }
}
