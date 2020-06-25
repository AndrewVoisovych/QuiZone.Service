using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.BusinessLogic.Services.Interfaces;
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
    public sealed class QuestionService : CrudService<QuestionDTO, Question>, IQuestionService
    {
         private readonly IMapper mapper;
        public QuestionService(
            IUnitOfWork database, 
            ILoggerManager logger,
            IQuestionRepository questionRepository,
            IMapper mapper)
            : base(database, logger, questionRepository, mapper)
        {
            this.mapper = mapper;
        }


         public override async Task<QuestionDTO> UpdateAsync(int id, QuestionDTO value)
         {
            var existedTableObject = await database.QuestionRepository.GetByIdAsync(id);

            var tableObject = mapper.Map<QuestionDTO, Question>(value);

            tableObject.Id = id;
            tableObject.CreateDate = existedTableObject.CreateDate;


            var updatedTableObject = repository.Update(tableObject);
            bool isSaved = await database.SaveAsync();

            return isSaved
                ? mapper.Map<Question, QuestionDTO>(updatedTableObject)
                : null;
        }

        /// <summary>
        /// Get all question for select quiz
        /// </summary>
        /// <param name="quizId">Id select quiz</param>
        /// <returns>List questions (includes answers options)</returns>
        public async Task<IEnumerable<QuestionDTO>> GetAllAsync(int quizId)
        {
            var questions = await database.QuestionRepository
                .GetByCondition(c => c.QuizId == quizId)
                .ToListAsync();

            if (questions.Any())
            {
                Random random = new Random();
                var questionsDTO = mapper.Map<IEnumerable<QuestionDTO>>(questions);
                foreach (QuestionDTO question in questionsDTO)
                {
                    if (question.CategoryId != 3 && question.CategoryId != 4)
                    {
                        var questionOptionsAnswers = await database.QuestionOptionsAnswerRepository
                        .GetByCondition(c => c.QuestionId == question.Id)
                        .Select(s => s.Answer)
                        .ToListAsync();

                        var questionCorrectAnswers = await database.QuestionCorrectAnswerRepository
                        .GetByCondition(c => c.QuestionId == question.Id)
                        .Select(s => s.Answer)
                        .ToListAsync();

                        var resultQuestionOptionsAnswers = mapper.Map<IEnumerable<AnswerDTO>>(questionOptionsAnswers);
                        var resultQuestionCorrectAnswers = mapper.Map<IEnumerable<AnswerDTO>>(questionCorrectAnswers);

                        question.Answers = resultQuestionCorrectAnswers
                            .Concat(resultQuestionOptionsAnswers)
                            .OrderBy(o => random.Next());
                    }
                    else
                    {
                        // temp logic
                       
                        var enumerableAnswerBody = new[] { new AnswerDTO()
                            {
                                Body = ""
                            }
                        };

                        question.Answers = enumerableAnswerBody;

                    }
                }

                return questionsDTO;

            }

            return null;
        }

        public async Task<int> GetCountQuestionFromQuiz(int quizId)
        {
            var questions = await database.QuestionRepository
               .GetByCondition(c => c.QuizId == quizId)
               .CountAsync();

            return questions;
        }
    }
}
