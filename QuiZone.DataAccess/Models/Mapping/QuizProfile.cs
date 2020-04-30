using AutoMapper;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizDTO>()
               .ForMember(m => m.Category, x => x.MapFrom(src => src.Category.Category))
               .ForMember(m => m.Topic, x => x.MapFrom(src => src.Topic.Topic))
               .ForMember(m => m.SettingId, x => x.MapFrom(src => src.SettingId));

            CreateMap<QuizDTO, Quiz>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.Category, opt => opt.Ignore())
                .ForMember(m => m.Topic, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore())
                .ForMember(m => m.CreateUserId, opt => opt.Ignore());
        }
    }
}
