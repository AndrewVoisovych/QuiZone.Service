using QuiZone.BusinessLogic.Services.Base;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuiZone.BusinessLogic.Services.Interfaces
{
    public interface IQuestionService: ICrudService<QuestionDTO, Question>
    {
        Task<IEnumerable<QuestionDTO>> GetAllAsync(int quizId);
        Task<int> GetCountQuestionFromQuiz(int quizId);
    }

    
}
