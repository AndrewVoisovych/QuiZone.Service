using System;
using System.Threading.Tasks;
using QuiZone.DataAccess.Repository.Interfaces;

namespace QuiZone.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IQuizRepository QuizRepository { get; }
        ITokenRepository TokenRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IQuestionCorrectAnswerRepository QuestionCorrectAnswerRepository { get; }
        IQuestionOptionsAnswerRepository QuestionOptionsAnswerRepository { get; }

        Task<bool> SaveAsync();
    }
}
