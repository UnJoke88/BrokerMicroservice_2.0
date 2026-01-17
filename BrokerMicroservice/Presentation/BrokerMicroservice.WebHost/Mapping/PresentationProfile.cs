using AutoMapper;
using BrokerMicroservice.Application.Models.Asset;
using BrokerMicroservice.Application.Models.Broker;
using BrokerMicroservice.Application.Models.Card;
using BrokerMicroservice.Application.Models.Client;
using BrokerMicroservice.Application.Models.Portfolio;
using BrokerMicroservice.Application.Models.Transaction;
using BrokerMicroservice.WebHost.Requests.Asset;
using BrokerMicroservice.WebHost.Requests.Broker;
using BrokerMicroservice.WebHost.Requests.Card;
using BrokerMicroservice.WebHost.Requests.Client;
using BrokerMicroservice.WebHost.Requests.Portfolio;
using BrokerMicroservice.WebHost.Requests.Transaction;
using BrokerMicroservice.WebHost.Responces.Asset;
using BrokerMicroservice.WebHost.Responces.Broker;
using BrokerMicroservice.WebHost.Responces.Card;
using BrokerMicroservice.WebHost.Responces.Client;
using BrokerMicroservice.WebHost.Responces.Portfolio;
using BrokerMicroservice.WebHost.Responces.Transaction;

namespace BrokerMicroservice.WebHost.Mapping
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<BrokerModel, BrokerShortResponce>();
            CreateMap<BrokerModel, BrokerDetailedResponce>();
            CreateMap<CreateBrokerRequest, CreateBrokerModel>();
            CreateMap<CreateBrokerModel, BrokerDetailedResponce>();
            CreateMap<UpdateBrokerRequest, BrokerModel>();

            CreateMap<ClientModel, ClientShortResponce>();
            CreateMap<ClientModel, ClientDetailedResponce>();
            CreateMap<CreateClientRequest, CreateClientModel>();
            CreateMap<CreateClientModel, ClientShortResponce>();
            CreateMap<UpdateClientRequest, ClientModel>();

            CreateMap<CardModel, CardShortResponce>();
            CreateMap<CardModel, CardDetailedResponce>();
            CreateMap<CreateCardRequest, CreateCardModel>();
            CreateMap<CreateCardModel, CardShortResponce>();
            CreateMap<UpdateCardRequest, CardModel>();

            CreateMap<AssetModel, AssetShortResponce>();
            CreateMap<AssetModel, AssetDetailedResponce>();
            CreateMap<CreateAssetRequest, CreateAssetModel>();
            CreateMap<CreateAssetModel, AssetShortResponce>();
            CreateMap<UpdateAssetRequest, AssetModel>();

            CreateMap<PortfolioModel, PortfolioShortResponce>();
            CreateMap<PortfolioModel, PortfolioDetailedResponce>();
            CreateMap<CreatePortfolioRequest, CreatePortfolioModel>();
            CreateMap<CreatePortfolioModel, PortfolioShortResponce>();
            CreateMap<UpdatePortfolioRequest, PortfolioModel>();

            CreateMap<TransactionModel, TransactionShortResponce>();
            CreateMap<TransactionModel, TransactionDetailedResponce>();
            CreateMap<CreateTransactionRequest, CreateTransactionModel>();
            CreateMap<CreateTransactionModel, TransactionShortResponce>();
            CreateMap<UpdateTransactionRequest, TransactionModel>();
        }
    }
}
