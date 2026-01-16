using AutoMapper;
using BrokerMicroservice.Application.Models.Broker;
using BrokerMicroservice.WebHost.Requests.Broker;
using BrokerMicroservice.WebHost.Responces.Broker;

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

            CreateMap<RouteModel, RouteShortResponce>();
            CreateMap<RouteModel, RouteDetailedResponce>();
            CreateMap<CreateRouteRequest, CreateRouteModel>();
            CreateMap<CreateRouteModel, RouteShortResponce>();

            CreateMap<AdministratorModel, AdministratorShortResponce>();
            CreateMap<AdministratorModel, AdministratorDetailedResponce>();
            CreateMap<CreateAdministratorRequest, CreateAdministratorModel>();
            CreateMap<CreateAdministratorModel, AdministratorShortResponce>();

        }
    }
}
