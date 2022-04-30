using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;
using Shared.RequestFeatures;
using System.Text.Json;

namespace DbManagerApi.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class ProductController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ProductController(IServiceManager serviceManager) =>
        _serviceManager = serviceManager;

    /// <summary>
    /// Get products list with parametrs
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns>Products list and metadate for pagination</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductParameters parameters)
    {
        
        var pagedResult = await _serviceManager.ProductService
            .GetProductsAsync(parameters, trackChanges: false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.products);
    }


    /// <summary>
    /// Get product by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>product item</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If the product with id doesn't exist in the database</response>
    [HttpGet("{id:guid}", Name = "ProductById")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(id, trackChanges: false);

        return Ok(product);
    }


    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>new product</returns>
    /// <response code="201">Returns created item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="422">If the model is invalid</response>
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationgDto product)
    {
        if (product is null)
            return BadRequest("ProductForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var createdProduct = await _serviceManager.ProductService.CreateProductAsync(product);

        return CreatedAtRoute("ProductById", new { createdProduct.id }, createdProduct);
    }


    /// <summary>
    /// Updates all fields of an existing product.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="product"></param>
    /// <returns>update product</returns>
    /// <response code="204">Update was successful</response>
    /// <response code="400">If the item is null</response>
    /// <response code="422">If the model is invalid</response>
    /// <response code="404">If the product with id doesn't exist in the database</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductForUpdateDto product)
    {
        if (product is null)
            return BadRequest("ProductForUpdateDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.ProductService.UpdateProductAsync(id, product, trackChanges: true);

        return NoContent();
    }


    /// <summary>
    /// Deleting an existing product by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204">Removal was successful</response>
    /// <response code="404">If the product with id doesn't exist in the database</response>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _serviceManager.ProductService.DeleteProductAsync(id, trackChanges: false);

        return NoContent();
    }


    [HttpGet("namesProduct")]
    public async Task<IActionResult> GetNamesProduct()
    {
       var result = await _serviceManager.ProductService.GetNamesProduct();

        return Ok(result);
    }
}
