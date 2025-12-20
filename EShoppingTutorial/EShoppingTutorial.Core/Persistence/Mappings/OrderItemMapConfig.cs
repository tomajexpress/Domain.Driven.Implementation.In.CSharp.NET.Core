using EShoppingTutorial.Core.Domain.Entities;
using EShoppingTutorial.Core.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShoppingTutorial.Core.Persistence.Mappings
{
    public class OrderItemMapConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(
                    id => id.Value,
                    value => new OrderItemId(value));

            builder.Property(o => o.Id).ValueGeneratedOnAdd().HasColumnName("Id");

            builder.Property(o => o.ProductId).HasColumnName("ProductId").IsRequired();

            builder.Property(o => o.ProductId).HasConversion(
                id => id.Value,
                value => new ProductId(value));

            builder.OwnsOne(o => o.Price, price =>
            {
                price.Property(x => x.Amount).HasColumnName("Amount");

                price.Property(x => x.Unit).HasColumnName("Unit");
            });

            // Configure OrderId as foreign key
            builder.Property(x => x.OrderId)
                .HasConversion(
                    id => id.Value,
                    value => new OrderId(value));
        }
    }
}
