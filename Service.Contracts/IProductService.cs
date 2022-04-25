using Shared.DataTransferObject;

namespace Service.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(bool trackChanges);
    Task<ProductDto> GetProductAsync(Guid productId, bool trackChanges);
    Task<ProductDto> CreateProductAsync(ProductForCreationgDto product);
    Task DeleteProductAsync(Guid productId, bool trackChanges);
    Task UpdateProductAsync(Guid productId, ProductForUpdateDto productForUpate, bool trackChanges);
}

