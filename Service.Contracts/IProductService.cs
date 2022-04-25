using Shared.DataTransferObject;

namespace Service.Contracts;

public interface IProductService
{
    IEnumerable<ProductDto> GetAllProducts(bool trackChanges);
    ProductDto GetProduct(Guid id, bool trackChanges);
    ProductDto CreateProduct(ProductForCreationgDto product);
    void DeleteProduct(Guid id, bool trackChanges);
    void UpdateProduct(Guid id, ProductForUpdateDto productForUpate, bool trackChanges);
}

