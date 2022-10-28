using PrivateEvents.Entities.Models;
using PrivateEvents.Entities.DataTransferObjects;
using AutoMapper;

namespace PrivateEvents;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegistrationModel, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        
        CreateMap<CreateEventDto,Event>();
    }
}