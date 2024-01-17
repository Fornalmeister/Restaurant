using AutoMapper;
using Services.Customer.Models;
using Services.Customer.Models.Dto;

namespace Services.Customer
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CustomersDto, Customers>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
