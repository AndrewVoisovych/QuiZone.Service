using Microsoft.EntityFrameworkCore;
using QuiZone.DataAccess.Models.Entities;
using QuiZone.DataAccess.Repository.Interfaces;
using System.Linq;

namespace QuiZone.DataAccess.Repository.Implemented
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(QuiZoneContext context)
           : base(context)
        {
        }

        protected override IQueryable<User> ComplexEntities => Entities.Include(u => u.Role);
    }
}
