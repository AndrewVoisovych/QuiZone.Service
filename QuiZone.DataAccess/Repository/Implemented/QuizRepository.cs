using Microsoft.EntityFrameworkCore;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using System.Linq;

namespace QuiZone.DataAccess.Repository.Implemented
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(QuiZoneContext context)
           : base(context)
        {
        }

        protected override IQueryable<Quiz> ComplexEntities => Entities
            .Include(e => e.Category)
            .Include(e => e.CreateUser)
            .Include(e => e.Topic)
            .Include(e => e.Setting)
            .Include(e => e.AccessNavigation);
    }


}
