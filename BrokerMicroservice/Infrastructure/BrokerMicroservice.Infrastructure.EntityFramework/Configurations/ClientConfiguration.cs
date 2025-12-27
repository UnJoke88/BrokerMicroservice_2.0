using BrokerMicroservice.Domain.Entities;
using BrokerMicroservise.ValueObgect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrokerMicroservice.Infrastructure.EntityFramework.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id); // Устанавливает первичный ключ
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName).IsRequired()
                .HasConversion(
                    v => v.Value,
                    v => new FirstName(v)); // Конвертация ValueObject

            builder.Property(x => x.LastName).IsRequired()
                .HasConversion(v => v.Value, v => new LastName(v));

            builder.Property(x => x.MiddleName).IsRequired(false)
                .HasConversion(v => v.Value, v => new MiddleName(v));

            builder.Property(x => x.Email).IsRequired()
                .HasConversion(v => v.Value, v => new Email(v));

            builder.Property(x => x.PhoneNumber).IsRequired()
                .HasConversion(
                    v => v.Value,
                    v => new PhoneNumber(v));

            // ... остальной код

            builder.HasOne(x => x.Broker).WithMany()
                .HasForeignKey(x => x.BrokerId)
                .IsRequired();


            // Внешний ключ и связь с Card (один к одному)
            builder.HasOne(x => x.Card).WithOne()
                .HasForeignKey<Client>("CardId")
                .IsRequired();

            // Внешний ключ и связь с Portfolio (один к одному)
            builder.HasOne(x => x.Portfolio).WithOne()
                .HasForeignKey<Client>("PortfolioId")
                .IsRequired();

            //Не учитывать это свойство при построении таблицы в базе данных (Не создавать для него колонку или навигацию).
            builder.Ignore(x => x.ShowTransactions);

        }
    }
}
