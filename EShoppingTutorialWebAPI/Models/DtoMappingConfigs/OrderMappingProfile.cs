using AutoMapper;
using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorialWebAPI.Models.OrderModels;
using System.Collections.Generic;

namespace EShoppingTutorialWebAPI.Models.DtoMappingConfigs
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderViewModel>();

            CreateMap<OrderSaveRequestModel, Order>()
            .ConstructUsing((src, res) =>
            {
                return new Order(
                    orderItems: res.Mapper.Map<IEnumerable<OrderItem>>(src.OrderItemsDtoModel)
                );
            });

            CreateMap<OrderItemSaveRequestModel, OrderItem>();

            CreateMap<OrderItem, OrderItemViewModel>();
        }
    }
}
