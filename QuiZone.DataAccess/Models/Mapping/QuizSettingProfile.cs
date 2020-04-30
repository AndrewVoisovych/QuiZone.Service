using AutoMapper;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.DataAccess.Models.Mapping
{
    public sealed class QuizSettingProfile : Profile
    {
        public QuizSettingProfile()
        {
            CreateMap<QuizSetting, QuizSettingsDTO>();


            CreateMap<QuizSettingsDTO, QuizSetting>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.CreateUserId, opt => opt.Ignore());

        }
    }
}
