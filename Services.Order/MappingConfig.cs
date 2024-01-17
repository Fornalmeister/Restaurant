using AutoMapper;
using Services.Order.Models;
using Services.Order.Models.Dto;

namespace Services.Order
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrdersDto, Orders>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
