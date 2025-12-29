using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShoppingTutorial.Core.Persistence.Mappings;

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

        builder.Property(o => o.TrackingNumber).HasColumnName("TrackingNumber").IsRequired(true);

        builder.HasIndex(o => o.TrackingNumber).IsUnique();

        builder.Property(o => o.ShippingAddress).HasColumnName("ShippingAddress").HasMaxLength(100).IsUnicode().IsRequired();

        builder.Property(o => o.OrderDate).HasColumnName("OrderDate").HasMaxLength(10).IsRequired();

        builder.Property(o => o.CustomerId).HasColumnName("CustomerId").IsRequired();

        builder.Property(o => o.CustomerId).HasConversion(
            id => id.Value,
            value => new CustomerId(value));

        builder.Property(o => o.OrderStatus).HasColumnName("OrderStatus").IsRequired();

        builder.HasMany(o => o.OrderItems)
               .WithOne()
               .HasForeignKey(x=> x.OrderId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
