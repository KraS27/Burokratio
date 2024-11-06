using Application.DTO.Notar;
using AutoMapper;
using Core.Entities;

namespace Application.Extensions.Automapper.Notars;

public class NotarMappingProfile : Profile
{
    public NotarMappingProfile()
    {
        CreateMap<Notar, NotarResponse>()
            .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("division", opt => opt.MapFrom(src => src.Address.Division))
            .ForCtorParam("country", opt => opt.MapFrom(src => src.Address.Country))
            .ForCtorParam("city", opt => opt.MapFrom(src => src.Address.City))
            .ForCtorParam("street", opt => opt.MapFrom(src => src.Address.Street))
            .ForCtorParam("postalCode", opt => opt.MapFrom(src => src.Address.PostalCode))
            .ForCtorParam("latitude", opt => opt.MapFrom(src => src.Coordinates.Latitude))
            .ForCtorParam("longitude", opt => opt.MapFrom(src => src.Coordinates.Longitude))
            .ForCtorParam("email", opt => opt.MapFrom(src => src.Email.Value))
            .ForCtorParam("phoneNumber", opt => opt.MapFrom(src => src.PhoneNumber.Value));
    }
}