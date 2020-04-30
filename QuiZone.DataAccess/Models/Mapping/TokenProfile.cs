using AutoMapper;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Models.DTO;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<TokenDTO, Token>()
                .ForMember(m => m.ModUserId, opt => opt.Ignore())
                .ForMember(m => m.CreateUserId, opt => opt.Ignore())
                .ForMember(m => m.ModDate, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore())
                .ForMember(m => m.RefreshToken, opt => opt.Ignore());
            CreateMap<Token, TokenDTO>()
                .ForMember(m => m.AccessToken, opt => opt.Ignore());
        }        
    }
}
