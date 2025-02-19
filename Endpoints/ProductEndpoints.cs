using Zeiss.ProductApi.Models;
using Zeiss.ProductApi.Services;
using Zeiss.ProductApi.Validators;

namespace Zeiss.ProductApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGet("/api/products", async (IProductService productService) =>
            {
                var products = await productService.GetAllProductsAsync();
                return Results.Ok(products);
            });

            app.MapGet("/api/products/{id}", async (int id, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });

            app.MapPost("/api/products", async (Product product, IProductService productService) =>
            {

                if (!ProductValidator.IsValidProduct(product))
                {
                    return Results.BadRequest("Invalid product data.");
                }

                product.CreatedAt = DateTime.UtcNow;
                await productService.AddProductAsync(product);
                return Results.Created($"/api/products/{product.Id}", product);
            });

            app.MapPut("/api/products/{id}", async (int id, Product product, IProductService productService) =>
            {

                if (id != product.Id)
                    return Results.BadRequest("Product ID mismatch.");

                var existingProduct = await productService.GetProductByIdAsync(id);
                if (existingProduct == null)
                {
                    return Results.NotFound();
                }

                await productService.UpdateProductAsync(product);
                return Results.NoContent();
            });

            app.MapDelete("/api/products/{id}", async (int id, IProductService productService) =>
            {
                var product = await productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return Results.NotFound();
                }

                await productService.DeleteProductAsync(id);
                return Results.NoContent();
            });

            app.MapPut("/api/products/decrement-stock/{id}/{quantity}", async (int id, int quantity, IProductService productService) =>
            {
                if (quantity <= 0)
                {
                    return Results.BadRequest("Quantity must be positive.");
                }

                await productService.DecrementStockAsync(id, quantity);
                return Results.Ok();
            });

            app.MapPut("/api/products/add-to-stock/{id}/{quantity}", async (int id, int quantity, IProductService productService) =>
            {
                if (quantity <= 0)
                {
                    return Results.BadRequest("Quantity must be positive.");
                }

                await productService.AddToStockAsync(id, quantity);
                return Results.Ok();
            });
        }
    }
}
