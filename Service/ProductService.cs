using AutoMapper;
using Contracts;
using Service.Contracts;
using Entities.Exceptions;
using Shared.DataTransferObject;
using Entities.Models;

namespace Service;

internal sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public ProductService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateProductAsync(ProductForCreationgDto product)
    {
        var productEntity = _mapper.Map<Product>(product);

        _repository.Product.CreateProduct(productEntity);
        await _repository.SaveAsync();

        var productToReturn = _mapper.Map<ProductDto>(productEntity);

        return productToReturn; 
    }

    public async Task DeleteProductAsync(Guid productId, bool trackChanges)
    {
        var product = await _repository.Product.GetByIdAsync(productId, trackChanges);
        if(product is null)
            throw new ProductNotFoundException(productId);

        _repository.Product.DeleteProduct(product);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(bool trackChanges)
    {
           //throw new Exception("Exception");
            var products = await _repository.Product.GetAllCompaniesAsync(trackChanges);

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return productDto;
    }

    public async Task<ProductDto> GetProductAsync(Guid id, bool trackChanges)
    {
        var product = await _repository.Product.GetByIdAsync(id, trackChanges);
        if (product == null)
            throw new ProductNotFoundException(id);

        var productDto = _mapper.Map<ProductDto>(product);

        return productDto;
    }

    public async Task UpdateProductAsync(Guid id, ProductForUpdateDto productForUpate, bool trackChanges)
    {
        var productEntity = _repository.Product.GetByIdAsync(id, trackChanges);

        if (productEntity is null)
            throw new ProductNotFoundException(id);

        _mapper.Map(productForUpate, productEntity);
       await _repository.SaveAsync();
    }
}
