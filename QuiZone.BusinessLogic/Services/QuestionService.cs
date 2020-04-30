using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuiZone.BusinessLogic.Services.Base;
using QuiZone.BusinessLogic.Services.Interfaces;
using QuiZone.Common.LoggerService;
using QuiZone.DataAccess.Models.DTO;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using QuiZone.DataAccess.UnitOfWork;
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

    
       
    
    }
}
