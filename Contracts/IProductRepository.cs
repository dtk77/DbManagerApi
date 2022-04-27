using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IProductRepository
{
    Task<PagedList<Product>> GetProductAsync(ProductParameters parameters, bool trackChanges);
    Task<Product?> GetByIdAsync(Guid productId, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);
}
