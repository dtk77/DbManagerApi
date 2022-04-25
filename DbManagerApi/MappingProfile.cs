using AutoMapper;
using Entities.Models;
using Shared.DataTransferObject;

namespace DbManagerApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductForCreationgDto, Product>();
        CreateMap<ProductForUpdateDto, Product>();
    }
} 
