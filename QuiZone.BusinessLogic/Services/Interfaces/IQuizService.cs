using System.Collections.Generic;
using System.Threading.Tasks;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.BusinessLogic.Services.Interfaces
{
    /// <summary>
    /// Answer model CRUD
    /// </summary>
    public interface IQuizService : ICrudService<QuizDTO, Quiz>
    {
        Task<IEnumerable<QuestionDTO>> GetQuestionByQuizAsync(int id);
        public string GetEndLinkHash(int userId, int quizId);
    }   
}
