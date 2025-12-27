using BrokerMicroservice.Domain.Entities;
using BrokerMicroservise.ValueObgect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokerMicroservice.Infrastructure.EntityFramework.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id); // Устанавливает первичный ключ
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Date).IsRequired().HasConversion
            (
                src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)// Преобразование времени в UTC
            );

            builder.Property(x => x.Type).IsRequired(); // Enum: TransactionType

            builder.Property(x => x.Amount).IsRequired()
                .HasConversion(v => v.Value, v => new Money(v)); // Конвертация ValueObject

            builder.Property(x => x.EndBalance).IsRequired()
                .HasConversion(v => v.Value, v => new Money(v)); // Конвертация ValueObject

            builder.Property(x => x.Status).IsRequired(); // Enum: TransactionStatus

            builder.Property(x => x.Quantity).IsRequired(false)
                .HasConversion(v => v.Value, v => new Quantity(v)); // Nullable Quantity для активных операций

            // Связь с клиентом (обязательная)
            builder.HasOne(x => x.Client).WithMany("_transactions");

            // Связь с активом (необязательная, т.к. может быть null при пополнении/снятии)
            builder.HasOne(x => x.Asset).WithMany().IsRequired(false);

            // Автозагрузка клиента
            builder.Navigation(x => x.Client).AutoInclude();

            // Автозагрузка актива, если операция касается активов
            builder.Navigation(x => x.Asset).AutoInclude();
        }
    }
}
