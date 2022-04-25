using Contracts;
using Entities.Models;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext)
    {
    }

    public void CreateProduct(Product product) => Create(product);

    public void DeleteProduct(Product product) => Delete(product);

    public IEnumerable<Product> GetAllCompanies(bool trackChanges) =>
        GetAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToList();

    public Product? GetById(Guid productId, bool trackChanges) =>
        GetByCondition(p => p.Id.Equals(productId), trackChanges)
        .SingleOrDefault();
}
