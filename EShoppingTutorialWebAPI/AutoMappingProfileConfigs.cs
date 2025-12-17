using AutoMapper;
using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.ValueObjects;
using EShoppingTutorialWebAPI.Models.OrderModels;
using System.Collections.Generic;

namespace EShoppingTutorialWebAPI
{
    internal static class AutoMappingProfileConfigs
    {
        internal static void AddAutoMapperConfigs(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            cfg.CreateMap<OrderSaveRequestModel, Order>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => new CustomerId(src.CustomerId)))
                .ConstructUsing((src, res) =>
                {
                    return new Order(
                        new CustomerId(src.CustomerId),
                        src.ShippingAdress,
                        orderItems: res.Mapper.Map<IEnumerable<OrderItem>>(src.OrderItemsDtoModel)
                    );
                });

            cfg.CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Value));

            cfg.CreateMap<OrderItemSaveRequestModel, OrderItem>();

            cfg.CreateMap<PriceSaveRequestModel, Price>().ConvertUsing(x => new Price(x.Amount, x.Unit.Value));
        }
    }
}