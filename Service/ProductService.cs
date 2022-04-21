using Contracts;
using Service.Contracts;


namespace Service;

internal sealed class ProductService : IProductService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public ProductService(IRepositoryManager repository, ILoggerManager logger) =>
        (_repository, _logger) = (repository, logger);
}
