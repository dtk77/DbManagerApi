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
    private readonly IHttpClientFactory _httpClientFactory;
    private string productsApiUrl = string.Empty;

    public ProductService(IHttpClientFactory httpClientFactory, IOptions<WebApiConfig> options)
    {
        if (options.Value.ProductApiUrl is null || options.Value.BaseUrl is null)
            throw new Exception($"Url api not set in configuration file");

        productsApiUrl = options.Value.ProductApiUrl;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<GroupViewModel> GetGroupModelProductsAsync(
                        int? pageNumber, int? pageSize, string? nameProduct)
    {
        var _httpClient = _httpClientFactory.CreateClient("ApiClient");

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
        var _httpClient = _httpClientFactory.CreateClient("ApiClient");

        var stringData = JsonSerializer.Serialize(product);

        var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(productsApiUrl, contentData);

        return response;
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id)
    {
        var _httpClient = _httpClientFactory.CreateClient("ApiClient");

        var response = await _httpClient.DeleteAsync($"{productsApiUrl}{id}");

        return response;
    }

    public async Task<Product> GetProductAsync(Guid id)
    {
        var _httpClient = _httpClientFactory.CreateClient("ApiClient");

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
        var _httpClient = _httpClientFactory.CreateClient("ApiClient");

        var stringData = JsonSerializer.Serialize(product);

        var contentData = new StringContent(stringData, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync(
            $"{productsApiUrl}{product.Id}", contentData);

        return response;
    }

    public async Task<List<SelectListItem>> GetNamesProductAsync(string selectedNameProduct)
    {
        var _httpClient = _httpClientFactory.CreateClient("ApiClient");

        HttpResponseMessage response = await _httpClient.GetAsync($"{productsApiUrl}namesProduct");

        var stringData = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        List<string>? listNames = JsonSerializer.Deserialize<List<string>>(stringData, options);

        List<SelectListItem> selectList =
            (listNames.Select(x => new SelectListItem() { Text = x, Value = x })).ToList();

        SetSelectedItemInList(selectList, selectedNameProduct);

        return selectList;
    }

    private static void SetSelectedItemInList(List<SelectListItem> selectList, string? selectedNameProduct)
    {
        var selectedItem = selectList.Find(x => x.Value == selectedNameProduct);
        if (selectedItem != null)
            selectedItem.Selected = true;
    }
}
