using AutoMapper;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDTO>()
                .ForMember(m => m.Category, x => x.MapFrom(src => src.Category.Category));

            CreateMap<QuestionDTO, Question>()
                    .ForMember(m => m.Id, opt => opt.Ignore())
                    .ForMember(m => m.CreateDate, opt => opt.Ignore())
                    .ForMember(m => m.CreateUserId, opt => opt.Ignore())
                    .ForMember(m => m.ModDate, opt => opt.Ignore());

        }
    }
}
