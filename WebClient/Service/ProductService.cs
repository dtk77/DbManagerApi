using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using WebClient.Contract;
using WebClient.Helpers;
using WebClient.Models;
using static WebClient.Helpers.HeaderParser;

namespace WebClient.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private string productsApiUrl = string.Empty;

    public ProductService(IHttpClientFactory httpClientFactory, IOptions<WebApiConfig> options)
    {
        productsApiUrl = options.Value.ProductApiUrl;
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<GroupViewModel> GetGroupModelProductsAsync(
                        int? pageNumber, int? pageSize, string? nameProduct)
    {
        var model = new GroupViewModel();

        var queryString =
            $"api/product?pageNumber={pageNumber}&pageSize={pageSize}&nameProduct={nameProduct}";
        var response = await _httpClient.GetAsync(queryString).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var productsString = await response.Content.ReadAsStringAsync();
        model.products = JsonSerializer.Deserialize<List<ProductViewModel>>(productsString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        PaginationInfo? xPag = FindAndParsePaginationInfo(response.Headers);
        model.paginationInfo = xPag;

        return model;
    }

    public async Task<HttpResponseMessage> CreateAsync(Product product)
    {
        var stringData = JsonSerializer.Serialize(product);
        var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(productsApiUrl, contentData);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id) =>
         await _httpClient.DeleteAsync($"{productsApiUrl}{id}");

    public async Task<Product> GetProductAsync(Guid id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{productsApiUrl}{id}");
        string stringData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        Product product = JsonSerializer.Deserialize<Product>(stringData, options);

        return (product);
    }

    public async Task<HttpResponseMessage> UpdateAsync(Product product)
    {
        var stringData = JsonSerializer.Serialize(product);
        var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync(
            $"{productsApiUrl}{product.Id}", contentData);

        return response;
    }

    public async Task<List<SelectListItem>> GetNamesProductAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{productsApiUrl}namesProduct");
        var stringData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        List<string>? listNames = JsonSerializer.Deserialize<List<string>>(stringData, options);

        List<SelectListItem> result = 
            (listNames.Select(x => new SelectListItem() { Text = x, Value = x })).ToList();

        return result;
    }
}
