using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;

namespace QuiZone.DataAccess.Repository.Implemented
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(QuiZoneContext context)
           : base(context)
        {
        }
    }
}
