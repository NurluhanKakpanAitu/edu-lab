using Application.Users.Vm;
using AutoMapper;

namespace Application.Users.MappingProfiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<Domain.Entities.User, UserVm>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email))
            .ForMember(x => x.Nickname, opt => opt.MapFrom(x => x.Nickname))
            .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role))
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt))
            .ForMember(x => x.PhotoPath, opt => opt.MapFrom(x => x.PhotoPath))
            .ForMember(x => x.About, opt => opt.MapFrom(x => x.About));
    }
}