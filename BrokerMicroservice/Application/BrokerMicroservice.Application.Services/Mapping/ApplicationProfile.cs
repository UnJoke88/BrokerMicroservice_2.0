using AutoMapper;
using BrokerMicroservice.Application.Models.Card;
using BrokerMicroservice.Domain.Entities;

namespace BrokerMicroservice.Application.Services.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Card, CardModel>()
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CardNumber))
                .ForMember(dest => dest.CashBalance, opt => opt.MapFrom(src => src.CashBalance));
        }
    }
}
