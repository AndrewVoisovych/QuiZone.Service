using QuiZone.BusinessLogic.Services.Base;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.BusinessLogic.Services.Interfaces
{
    public interface IQuestionService: ICrudService<QuestionDTO, Question>
    {
      

    }

    
}
