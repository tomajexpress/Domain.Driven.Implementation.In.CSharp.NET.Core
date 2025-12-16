using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShoppingTutorial.Core.Persistence.Mappings
{
    public class OrderMapConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasConversion(
                id => id.Value,
                value => new OrderId(value));

            builder.Property(o => o.Id).ValueGeneratedOnAdd().HasColumnName("Id");

            builder.Property(en => en.TrackingNumber).HasColumnName("TrackingNumber").IsRequired(false);

            builder.HasIndex(en => en.TrackingNumber).IsUnique();

            builder.Property(en => en.ShippingAdress).HasColumnName("ShippingAdress").HasMaxLength(100).IsUnicode().IsRequired();

            builder.Property(en => en.OrderDate).HasColumnName("OrderDate").HasMaxLength(10).IsRequired();
        }
    }
}
