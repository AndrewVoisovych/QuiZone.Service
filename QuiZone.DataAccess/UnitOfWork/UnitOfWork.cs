using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuiZone.Common.GlobalErrorHandling;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess.Repository.Implemented;
using QuiZone.DataAccess.Repository.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QuiZone.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuiZoneContext context;
        private readonly ILoggerManager logger;


        private UserRepository userRepository;
        private QuizRepository quizRepository;
        private TokenRepository tokenRepository;
        private AnswerRepository answerRepository;
        private QuestionRepository questionRepository;


        public UnitOfWork(QuiZoneContext context, ILoggerManager logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IUserRepository UserRepository => userRepository ??= new UserRepository(context);
        public IQuizRepository QuizRepository => quizRepository ??= new QuizRepository(context);
        public ITokenRepository TokenRepository => tokenRepository ??= new TokenRepository(context);
        public IAnswerRepository AnswerRepository => answerRepository ??= new AnswerRepository(context);
        public IQuestionRepository QuestionRepository => questionRepository ??= new QuestionRepository(context);


        public async Task<bool> SaveAsync()
        {
            try
            {
                var changes = context.ChangeTracker.Entries().Count(
                    p => p.State == EntityState.Modified || p.State == EntityState.Deleted
                                                         || p.State == EntityState.Added);

                return changes == 0 || await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException e)
            {
                var sqlExc = e.GetBaseException() as SqlException;
                if (sqlExc?.Number == 547)
                {
                    throw new HttpException(HttpStatusCode.InternalServerError, "У вибраного об'єкта є залежності, спочатку видаліть їх.");
                }   
                throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
               
            }
            catch (Exception e)
            {
                logger.Error($"{e}, {nameof(SaveAsync)}");
                return false;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }


    }
}
