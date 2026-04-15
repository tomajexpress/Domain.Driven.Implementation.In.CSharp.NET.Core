using EShoppingTutorial.Core.Application.Products.Queries;

namespace EShoppingTutorial.Core.Application.Products;

public static class ProductMappingProfile
{
    public static void AddMappingConfigs(IMapperConfigurationExpression cfg)
    {
        // Konvertierung der stark typisierten ID (falls nicht schon global deklariert)
        cfg.CreateMap<ProductId, int>().ConvertUsing(id => id.Value);

        cfg.CreateMap<Product, ProductViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(src => src.Price.Value))
            .ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => src.Price.Currency.ToString()));
    }
}