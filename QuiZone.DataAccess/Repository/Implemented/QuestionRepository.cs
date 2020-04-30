using Microsoft.EntityFrameworkCore;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using System.Linq;

namespace QuiZone.DataAccess.Repository.Implemented
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(QuiZoneContext context)
           : base(context)
        {
        }

        protected override IQueryable<Question> ComplexEntities => Entities
            .Include(e => e.Quiz)
            .Include(e => e.Category);
     
    }


}