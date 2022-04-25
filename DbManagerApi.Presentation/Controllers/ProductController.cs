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
    public IActionResult GetProducts()
    {
            var products = _serviceManager.ProductService.GetAllProducts(trackChanges: false);

            return Ok(products);
    }

    [HttpGet("{id:guid}", Name = "ProductById")]
    public IActionResult GetProduct(Guid id)
    {
        var product = _serviceManager.ProductService.GetProduct(id, trackChanges: false);

        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] ProductForCreationgDto product)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        if (product is null)
            return BadRequest("ProductForCreationDto object is null");

        var createdProduct = _serviceManager.ProductService.CreateProduct(product);

        return CreatedAtRoute("ProductById", new { id = createdProduct.id }, createdProduct);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteProduct(Guid id)
    {
        _serviceManager.ProductService.DeleteProduct(id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateProduct(Guid id, [FromBody] ProductForUpdateDto product)
    {
        if (product is null)
            return BadRequest("ProductForUpdateDto object is null");
        
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _serviceManager.ProductService.UpdateProduct(id, product, trackChanges: true);

        return NoContent();
        
    }

}
