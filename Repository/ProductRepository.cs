using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext)
    {
    }

    public void CreateProduct(Product product) => Create(product);

    public void DeleteProduct(Product product) => Delete(product);

    public async Task<IEnumerable<Product>> GetAllCompaniesAsync(bool trackChanges) =>
        await GetAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid productId, bool trackChanges) =>
       await GetByCondition(p => p.Id.Equals(productId), trackChanges)
        .SingleOrDefaultAsync();
}
