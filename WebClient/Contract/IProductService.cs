using Microsoft.AspNetCore.Mvc.Rendering;
using WebClient.Models;

namespace WebClient.Contract;

public interface IProductService
{
    Task<GroupViewModel> GetGroupModelProductsAsync(
            int? pageNumber, int? pageSize, string? nameProduct);
    Task<HttpResponseMessage> CreateAsync(Product product);
    Task<HttpResponseMessage> DeleteAsync(Guid id);
    Task<HttpResponseMessage> UpdateAsync(Product product);
    Task<Product> GetProductAsync(Guid id);
    Task<List<SelectListItem>> GetNamesProductAsync();
}
