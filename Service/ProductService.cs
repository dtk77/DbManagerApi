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

    public ProductDto CreateProduct(ProductForCreationgDto product)
    {
        var productEntity = _mapper.Map<Product>(product);

        _repository.Product.CreateProduct(productEntity);
        _repository.Save();

        var productToReturn = _mapper.Map<ProductDto>(productEntity);

        return productToReturn; 
    }

    public void DeleteProduct(Guid id, bool trackChanges)
    {
        var product = _repository.Product.GetById(id, trackChanges);
        if(product is null)
            throw new ProductNotFoundException(id);

        _repository.Product.DeleteProduct(product);
        _repository.Save();
    }

    public IEnumerable<ProductDto> GetAllProducts(bool trackChanges)
    {
           //throw new Exception("Exception");
            var companies = _repository.Product.GetAllCompanies(trackChanges);

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(companies);

            return productDto;
    }

    public ProductDto GetProduct(Guid id, bool trackChanges)
    {
        var product = _repository.Product.GetById(id, trackChanges);
        if (product == null)
            throw new ProductNotFoundException(id);

        var productDto = _mapper.Map<ProductDto>(product);

        return productDto;
    }

    public void UpdateProduct(Guid id, ProductForUpdateDto productForUpate, bool trackChanges)
    {
        var product = _repository.Product.GetById(id, trackChanges);

        if (product is null)
            throw new ProductNotFoundException(id);

        _mapper.Map(productForUpate, product);
        _repository.Save();
    }
}
