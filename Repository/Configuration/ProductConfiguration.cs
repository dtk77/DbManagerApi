using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
       /* builder.HasData
         (
            new Product
            {
                Id = new Guid("3d11132e-6371-4e30-8a8d-3738eb99dbbf"),
                Name = "Product 1",
                Description = "Description product 1"
            },
            new Product
            {
                Id = new Guid("8a997804-7308-45a8-a3e6-ed65bfab46e8"),
                Name = "Product 2",
                Description = "Description product 2"
            },
             new Product
             {
                 Id = new Guid("8a997804-0308-45a8-a3e6-ed64bfab46e9"),
                 Name = "Product 3",
                 Description = "Description product 3"
             }
        );*/
    }
}
