using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShoppingTutorial.Core.Persistence.Mappings;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Define the table name
        builder.ToTable("Products");

        // Primary Key configuration
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value))
            .ValueGeneratedOnAdd()
            .HasColumnName("Id");

        // Basic property mappings
        builder.Property(p => p.Name)
            .HasColumnName("Name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("Description")
            .HasMaxLength(500);

        // This flattens the Price properties directly into the Products table
        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(x => x.Value)
                .HasColumnName("Price_Value")
                .HasPrecision(18, 2) // Recommended for financial data
                .IsRequired();

            price.Property(x => x.Currency)
                .HasColumnName("Price_Currency")
                .IsRequired();
        });
    }
}