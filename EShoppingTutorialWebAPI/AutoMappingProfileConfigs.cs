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
            cfg.CreateMap<Order, OrderViewModel>();
            cfg.CreateMap<OrderSaveRequestModel, Order>()
            .ConstructUsing((src, res) =>
            {
                return new Order(src.ShippingAdress, orderItems: res.Mapper.Map<IEnumerable<OrderItem>>(src.OrderItemsDtoModel)
                );
            });

            cfg.CreateMap<OrderItem, OrderItemViewModel>();

            cfg.CreateMap<OrderItemSaveRequestModel, OrderItem>();

            cfg.CreateMap<PriceSaveRequestModel, Price>().ConvertUsing(x => new Price(x.Amount.Value, x.Unit.Value));
        }
    }
}