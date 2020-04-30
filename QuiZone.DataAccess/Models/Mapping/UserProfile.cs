using AutoMapper;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
           .ForMember(m => m.RoleName, x => x.MapFrom(src => src.Role.Role));

            CreateMap<UserDTO, User>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Role, opt => opt.Ignore())
                .ForMember(m => m.Password, opt => opt.Ignore());
        }
    }
}
