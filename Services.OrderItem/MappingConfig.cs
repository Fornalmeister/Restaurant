using AutoMapper;
using Services.OrderItem.Models;
using Services.OrderItem.Models.Dto;

namespace Services.OrderItem
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderItemsDto, OrderItems>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
