using MongoDbWithMinimalApi.Data;
using MongoDbWithMinimalApi.Model;

namespace MongoDbWithMinimalApi.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext _productDbContext;
        public ProductService(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public Product Create(Product product)
        {
            product.Id = GenerateNewId();

            _productDbContext.Add(product);
            _productDbContext.SaveChanges();
            return product;
        }

        private string GenerateNewId()
        {
            return Guid.NewGuid().ToString();
        }
        public void Delete(string id)
        {
            var item = _productDbContext.Products.Find(id);
            if (item != null)
            {
                _productDbContext.Products.Remove(item);
                _productDbContext.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return _productDbContext.Products;
        }

        public Product GetById(string id)
        {
            return _productDbContext.Products.Find(id);
        }

        public void Update(string id, Product product)
        {
            var existingItem = _productDbContext.Products.Find(id);
            if (existingItem != null)
            {
                existingItem.Name = product.Name;
                existingItem.Description = product.Description;
                _productDbContext.SaveChanges();
            }
        }
    }
}
