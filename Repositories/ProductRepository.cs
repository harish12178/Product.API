using Microsoft.EntityFrameworkCore;
using Zeiss.ProductApi.Consts;
using Zeiss.ProductApi.Data;
using Zeiss.ProductApi.Models;
using Zeiss.ProductApi.Services;

namespace Zeiss.ProductApi.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext _dbContext;
        private readonly ISqlSequenceProvider _sequenceProvider;

        public ProductRepository(ProductDbContext dbContext, ISqlSequenceProvider sequenceProvider)
        {
            _dbContext = dbContext;
            _sequenceProvider = sequenceProvider;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _dbContext.Products.AsNoTracking().ToListAsync();

        public async Task<Product> GetProductByIdAsync(int id) =>
            await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<int> AddProductAsync(Product product)
        {
            product.Id = await _sequenceProvider.GetNextValueForSequenceAsync(AppConst.ProductSequenceName);
            product.CreatedAt = DateTime.Now;
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DecrementStockAsync(int id, int quantity)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null && product.Stock >= quantity)
            {
                product.Stock -= quantity;
                await UpdateProductAsync(product);
            }
        }

        public async Task AddToStockAsync(int id, int quantity)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                product.Stock += quantity;
                await UpdateProductAsync(product);
            }
        }
    }
}
