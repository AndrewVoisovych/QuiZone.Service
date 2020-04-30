using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;

namespace QuiZone.DataAccess.Repository.Implemented
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(QuiZoneContext context)
           : base(context)
        {
        }
    }
}
