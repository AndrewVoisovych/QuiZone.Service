using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.Common.Helpers;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using QuiZone.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuiZone.BusinessLogic.Services
{
    public sealed class QuizService : CrudService<QuizDTO, Quiz>, IQuizService
    {
        private readonly IMapper mapper;
        public QuizService(
            IUnitOfWork database,
            ILoggerManager logger,
            IQuizRepository quizRepository,
            IMapper mapper)
            : base(database, logger, quizRepository, mapper)
        {
            this.mapper = mapper;
        }

        public override async Task<QuizDTO> UpdateAsync(int id, QuizDTO value)
        {
            var existedTableObject = await database.QuizRepository.GetByIdAsync(id);
            if (existedTableObject == null)
            {
                return null;
            }

            var tableObject = mapper.Map<QuizDTO, Quiz>(value);

            tableObject.Id = id;
            tableObject.CreateUserId = existedTableObject.CreateUserId;
            tableObject.CreateDate = existedTableObject.CreateDate;


            var updatedTableObject = repository.Update(tableObject);
            bool isSaved = await database.SaveAsync();

            return isSaved
                ? mapper.Map<Quiz, QuizDTO>(updatedTableObject)
                : null;
        }

        public override async Task<IEnumerable<QuizDTO>> GetAllAsync()
        {
            var allQuiz = await repository
                .GetByCondition(c => c.AccessId == 1)
                .OrderByDescending(o => o.CreateDate)
                .ToListAsync();
                        
            return allQuiz == null
                ? null
                : mapper.Map<IEnumerable<QuizDTO>>(allQuiz);
        }


        public async Task<IEnumerable<QuestionDTO>> GetQuestionByQuizAsync(int id)
        {
            var questions = await database.QuestionRepository
                .GetByCondition(x => x.QuizId == id)
                .ToListAsync();

            return questions == null
                ? null
                : mapper.Map<IEnumerable<QuestionDTO>>(questions);
        }

        public string GetEndLinkHash(int userId, int quizId)
        {
            string lineForHash = $"{userId}#{quizId}#QuiZoneSecurityHash";
            string startHashPass = $"{SHA256Hash.ComputeString(lineForHash)}#QuiZoneSecurityHash";
            string reverseHash = new string(startHashPass.ToCharArray().Reverse().ToArray());
            string finalHashPass = SHA256Hash.ComputeString(reverseHash);

            return finalHashPass;
        }
    }
}
