using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;

namespace DbManagerApi.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ProductController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(trackChanges: false);

            return Ok(products);
    }

    [HttpGet("{id:guid}", Name = "ProductById")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _serviceManager.ProductService.GetProductAsync(id, trackChanges: false);

        return Ok(product);
    }

    [HttpPost]
     public async Task<IActionResult> CreateProduct([FromBody] ProductForCreationgDto product)
    {
        if (product is null)
            return BadRequest("ProductForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);


        var createdProduct = await _serviceManager.ProductService.CreateProductAsync(product);

        return CreatedAtRoute("ProductById", new {createdProduct.id }, createdProduct);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _serviceManager.ProductService.DeleteProductAsync(id, trackChanges: false);

        return NoContent();
    }

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

}
