using BrokerMicroservice.Domain.Entities;
using BrokerMicroservise.ValueObgect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokerMicroservice.Infrastructure.EntityFramework.Configurations
{
    public class PortfolioEntryConfiguration : IEntityTypeConfiguration<PortfolioEntry>
    {
        public void Configure(EntityTypeBuilder<PortfolioEntry> builder)
        {
            builder.HasKey(x => x.Id); // Первичный ключ
            builder.Property(x => x.Id).ValueGeneratedOnAdd(); // Автогенерация

            builder.Property(x => x.Quantity).IsRequired()
                .HasConversion(q => q.Value, v => new Quantity(v)); // ValueObject: Quantity

            builder.HasOne(x => x.Asset).WithMany()
                .HasForeignKey(x => x.AssetId)
                .IsRequired(); // Навигация к Asset

            builder.HasOne(x => x.Portfolio).WithMany("_entries")
                .HasForeignKey(x => x.PortfolioId)
                .IsRequired(); // Обратная связь к Portfolio
        }
    }
}
