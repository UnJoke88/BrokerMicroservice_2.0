using BrokerMicroservice.Domain.Entities;
using BrokerMicroservise.ValueObgect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokerMicroservice.Infrastructure.EntityFramework.Configurations
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(x => x.Id); // PK
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.PortfolioNumber).IsRequired()
                .HasConversion(v => v.Value, v => new PortfolioNumber(v));

            builder.HasMany<PortfolioEntry>("_entries").WithOne(x => x.Portfolio)
                .HasForeignKey(x => x.PortfolioId)
                .IsRequired(); // связь с PortfolioEntry

            builder.Ignore(x => x.AssetEntries); // геттер, не нужно мапить
            builder.Ignore(x => x.TotalValue);   // вычисляется по ходу
        }
    }
}
