using Application.DTO.Notar;
using AutoMapper;

namespace Application.Extensions.Automapper.Notar;

public class NotarMappingProfile : Profile
{
    public NotarMappingProfile()
    {
        CreateMap<Core.Entities.Notar, NotarResponse>()
            .ForMember(x => x.name, opt => opt.MapFrom(src => src.Name))
            .ForMember(x => x.division, opt => opt.MapFrom(src => src.Address.Division))
            .ForMember(x => x.country, opt => opt.MapFrom(src => src.Address.Country))
            .ForMember(x => x.city, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(x => x.street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(x => x.postalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
            .ForMember(x => x.latitude, opt => opt.MapFrom(src => src.Coordinates.Latitude))
            .ForMember(x => x.longitude, opt => opt.MapFrom(src => src.Coordinates.Longitude))
            .ForMember(x => x.email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(x => x.phoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value));
    }
}