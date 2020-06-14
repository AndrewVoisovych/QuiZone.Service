using AutoMapper;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Models.DTO;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class OptionsAnswerProfile : Profile
    {
        public OptionsAnswerProfile()
        {
            CreateMap<QuestionOptionsAnswer, OptionsAnswerDTO>()
                 .ForMember(m => m.Answer, x => x.MapFrom(src => src.Answer));

            CreateMap<OptionsAnswerDTO, QuestionOptionsAnswer>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}

