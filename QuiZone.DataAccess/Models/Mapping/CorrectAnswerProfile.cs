using AutoMapper;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Models.DTO;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class CorrectAnswerProfile : Profile
    {
        public CorrectAnswerProfile()
        {
            CreateMap<QuestionCorrectAnswer, CorrectAnswerDTO>()
                 .ForMember(m => m.Answer, x => x.MapFrom(src => src.Answer));

            CreateMap<CorrectAnswerDTO, QuestionCorrectAnswer>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}

