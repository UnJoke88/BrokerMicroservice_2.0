using AutoMapper;
using BrokerMicroservice.Application.Models.Asset;
using BrokerMicroservice.Application.Models.Broker;
using BrokerMicroservice.Application.Models.Card;
using BrokerMicroservice.Application.Models.Client;
using BrokerMicroservice.Application.Models.Portfolio;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.Domain.Entities;
using BrokerMicroservise.ValueObgect;

namespace BrokerMicroservice.Application.Services.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            //Маппинг money к decimal
            CreateMap<Money, decimal>().ConvertUsing(x => x.Value);

            //Карта
            CreateMap<Card, CardModel>()
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.CashBalance, opt => opt.MapFrom(src => src.CashBalance));

            //Активы
            CreateMap<Asset, AssetModel>()
                .ForMember(d => d.MinimalUnit, o => o.MapFrom(s => s.MinimalUnit))
                .ForMember(d => d.PurchasePrice, o => o.MapFrom(s => s.PurchasePrice.Value));

            //Брокер
            CreateMap<Broker, BrokerModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Value))
                .ForMember(d => d.Clients, o => o.MapFrom(s => s.ShowClients))
                .ForMember(d => d.Assets, o => o.MapFrom(s => s.ShowAsset));

            //Клиент
            CreateMap<Client, ClientModel>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName.Value))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName.Value))
                .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.MiddleName == null ? null : s.MiddleName.Value))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Value))
                .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber.Value))
                .ForMember(d => d.Transactions, o => o.MapFrom(s => s.ShowTransactions));

            //Портфель
            CreateMap<Portfolio, PortfolioModel>()
                .ForMember(d => d.PortfolioNumber, o => o.MapFrom(s => s.PortfolioNumber.Value))
                .ForMember(d => d.PortfolioTotalValue, o => o.MapFrom(s => s.PortfolioTotalValue))
                .ForMember(d => d.Entries, o => o.MapFrom(s => s.AssetEntries));

            //Вспомагательная сущность для отображения портфеля
            CreateMap<PortfolioEntry, PortfolioEntryModel>()
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity.Value))
                .ForMember(d => d.UnitPrice, o => o.MapFrom(s => s.Asset.PurchasePrice))
                .ForMember(d => d.TotalValue, o => o.MapFrom(s => s.Asset.PurchasePrice.Value * s.Quantity.Value));
            
            //Транзакции
            CreateMap<Transaction, TransactionModel>()
               .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity.Value))
               .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
               .ForMember(d => d.EndBalance, o => o.MapFrom(s => s.EndBalance));
               
        }
    }
}
