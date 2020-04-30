using AutoMapper;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Models.DTO;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerDTO>();
            CreateMap<AnswerDTO, Answer>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore());
        }
    }
}

