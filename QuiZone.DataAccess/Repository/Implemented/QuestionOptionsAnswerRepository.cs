using Microsoft.EntityFrameworkCore;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using System.Linq;

namespace QuiZone.DataAccess.Repository.Implemented
{
    public class QuestionOptionsAnswerRepository: BaseRepository<QuestionOptionsAnswer>, IQuestionOptionsAnswerRepository
    {
        public QuestionOptionsAnswerRepository(QuiZoneContext context)
           : base(context)
        {
        }

        protected override IQueryable<QuestionOptionsAnswer> ComplexEntities => Entities
         .Include(e => e.Answer);

    }
}
