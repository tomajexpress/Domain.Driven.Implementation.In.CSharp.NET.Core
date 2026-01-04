namespace EShoppingTutorial.Core.Application.Orders;

public static class OrderMappingProfile
{
    public static void AddMappingConfigs(IMapperConfigurationExpression cfg)
    {
        // OrderItemDto -> OrderItem
        cfg.CreateMap<OrderItemDto, OrderItem>()
           .ConstructUsing(src => new OrderItem(
               new ProductId(src.ProductId),
               new Price(src.Amount, Enum.Parse<Currency>(src.Currency))
           ));

        cfg.CreateMap<OrderId, int>().ConvertUsing(id => id.Value);
        cfg.CreateMap<ProductId, int>().ConvertUsing(id => id.Value);
        cfg.CreateMap<OrderItemId, int>().ConvertUsing(id => id.Value);

        cfg.CreateMap<Price, PriceDto>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()));

        // Entity -> ViewModel
        cfg.CreateMap<Order, OrderViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        // OrderItem -> OrderItemViewModel
        cfg.CreateMap<OrderItem, OrderItemViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Value))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
    }
}