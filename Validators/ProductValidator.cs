using Zeiss.ProductApi.Models;

namespace Zeiss.ProductApi.Validators
{
    public class ProductValidator
    {
        public static bool IsValidProduct(Product product)
        {
            return !string.IsNullOrEmpty(product.Name) && product.Price > 0 && product.Stock >= 0;
        }
    }
}
