using Shared.DataTransferObject;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IProductService
{
    Task<(IEnumerable<ProductDto> products, MetaData metaData)> GetProductsAsync(ProductParameters parameters, bool trackChanges);
    Task<ProductDto> GetProductAsync(Guid productId, bool trackChanges);
    Task<ProductDto> CreateProductAsync(ProductForCreationgDto product);
    Task DeleteProductAsync(Guid productId, bool trackChanges);
    Task UpdateProductAsync(Guid productId, ProductForUpdateDto productForUpate, bool trackChanges);

    Task<List<string>> GetNamesProduct();
}

