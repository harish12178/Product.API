using Zeiss.ProductApi.Models;
using Zeiss.ProductApi.Repositories;

namespace Zeiss.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            _logger.LogInformation("Fetching all products.");
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            _logger.LogInformation("Fetching product with ID: {Id}", id);
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            _logger.LogInformation("Adding a new product with Name: {Name}", product.Name);
            await _productRepository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _logger.LogInformation("Updating product with ID: {Id}", product.Id);
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            _logger.LogInformation("Deleting product with ID: {Id}", id);
            await _productRepository.DeleteProductAsync(id);
        }

        public async Task DecrementStockAsync(int id, int quantity)
        {
            _logger.LogInformation("Decrementing stock for product with ID: {Id} by {Quantity} units.", id, quantity);
            await _productRepository.DecrementStockAsync(id, quantity);
        }

        public async Task AddToStockAsync(int id, int quantity)
        {
            _logger.LogInformation("Adding {Quantity} units to stock for product with ID: {Id}.", quantity, id);
            await _productRepository.AddToStockAsync(id, quantity);
        }
    }
}
