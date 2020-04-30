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
           .ForMember(m => m.QuizId, x => x.MapFrom(src => src.Quiz.Id));

            CreateMap<QuestionDTO, Question>()
                    .ForMember(m => m.Id, opt => opt.Ignore())
                    .ForMember(m => m.CreateDate, opt => opt.Ignore())
                    .ForMember(m => m.CreateUserId, opt => opt.Ignore())
                    .ForMember(m => m.ModDate, opt => opt.Ignore());

        }
    }
}
