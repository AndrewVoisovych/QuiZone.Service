﻿using AutoMapper;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using QuiZone.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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


        public async Task<IEnumerable<QuestionDTO>> GetQuestionByQuizAsync(int id)
        {
            var questions = await database.QuestionRepository
                .GetByCondition(x => x.QuizId == id)
                .ToListAsync();

            return questions == null
                ? null
                : mapper.Map<IEnumerable<QuestionDTO>>(questions);
        }
    }
}