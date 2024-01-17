using AutoMapper;
using Services.Product.Models;
using Services.Product.Models.Dto;

namespace Services.Product
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Products>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
