using Entities.Models;

namespace Contracts;

public interface IProductRepository
{
    IEnumerable<Product> GetAllCompanies(bool trackChanges);
    Product GetById(Guid productId, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);
}
