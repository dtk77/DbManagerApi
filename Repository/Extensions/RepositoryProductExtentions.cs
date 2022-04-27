using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryProductExtentions
{
    public static IQueryable<Product> FilterProduct(this IQueryable<Product> products, string? filterName)
    {
        if (string.IsNullOrWhiteSpace(filterName))
            return products;

        return products.Where(x => x.Name == filterName);
    }
}
