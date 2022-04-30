using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using Repository.Extensions;

namespace Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Product>> GetProductsAsync(ProductParameters parameters, bool trackChanges)
    {
        var products = await GetAll(trackChanges)
            .FilterProduct(parameters.NameProduct)
            .OrderBy(c => c.Name)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        var count = await GetAll(trackChanges)
            .FilterProduct(parameters.NameProduct)
            .CountAsync();

        return PagedList<Product>.ToPagedList(products, count, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<Product?> GetByIdAsync(Guid productId, bool trackChanges) =>
       await GetByCondition(p => p.Id.Equals(productId), trackChanges)
        .SingleOrDefaultAsync();

    public void CreateProduct(Product product) => Create(product);

    public void DeleteProduct(Product product) => Delete(product);

    public async Task<List<string>> GetAllNamesProduct()
    {
       List<string>? namesProduct = await RepositoryContext.Products.Select(p => p.Name).Distinct().ToListAsync();

        return namesProduct;
    }
}
