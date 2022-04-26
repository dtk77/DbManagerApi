using Entities.Models;

namespace Contracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllCompaniesAsync(bool trackChanges);
    Task<Product?> GetByIdAsync(Guid productId, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);
}
