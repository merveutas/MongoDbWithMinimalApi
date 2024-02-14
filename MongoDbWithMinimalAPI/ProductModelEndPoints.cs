using Microsoft.AspNetCore.Http.HttpResults;
using MongoDbWithMinimalApi.Model;
using MongoDbWithMinimalApi.Service;

namespace MongoDbWithMinimalApi
{
    public static class ProductModelEndPoints
    {
        public static void MapProductModelEndPoints(this IEndpointRouteBuilder endpointRoute)
        {
            var group = endpointRoute.MapGroup("/api/Product").WithTags(nameof(Product));

            group.MapGet("/", async (IProductService productService) =>
            {
                var results = productService.GetAll();
                return results;
            }).WithName("GetAllProducts").WithOpenApi();


            group.MapGet("/{id}", async Task<Results<Ok<Product>, NotFound>> (string id, IProductService productService) =>
            {
                var item = productService.GetById(id);
                if (item == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(item);

            }).WithName("GetProductById").WithOpenApi();


            group.MapGet("/", async (Product product, IProductService productService) =>
            {
                var created = productService.Create(product);
                return TypedResults.Created($"/api/GetProductById/{product.Id}", product);

            }).WithName("GetProduct").WithOpenApi();


            group.MapGet("/{id}", async Task<Results<Ok, NotFound>> (string id, Product product, IProductService productService) =>
            {
                productService.Update(id, product);
                return TypedResults.Ok();

            }).WithName("UpdateProduct").WithOpenApi();


            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (string id, IProductService productService) =>
            {
                productService.Delete(id);
                return TypedResults.Ok();

            }).WithName("DeleteProduct").WithOpenApi();

        }
    }
}
