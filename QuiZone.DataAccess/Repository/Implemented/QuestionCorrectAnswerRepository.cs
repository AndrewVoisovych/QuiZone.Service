using Microsoft.EntityFrameworkCore;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using System.Linq;

namespace QuiZone.DataAccess.Repository.Implemented
{
    public class QuestionCorrectAnswerRepository: BaseRepository<QuestionCorrectAnswer>, IQuestionCorrectAnswerRepository
    {
        public QuestionCorrectAnswerRepository(QuiZoneContext context)
           : base(context)
        {
        }

        protected override IQueryable<QuestionCorrectAnswer> ComplexEntities => Entities
         .Include(e => e.Answer);

    }
}
