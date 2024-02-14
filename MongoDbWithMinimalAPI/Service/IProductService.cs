using MongoDbWithMinimalApi.Model;

namespace MongoDbWithMinimalApi.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(string id);
        Product Create(Product product);
        void Update(string id, Product product);
        void Delete(string id);


    }
}
