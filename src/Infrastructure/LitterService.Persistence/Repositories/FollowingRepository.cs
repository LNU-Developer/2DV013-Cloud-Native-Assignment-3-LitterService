using LitterService.Application.Contracts.Persistence;
using LitterService.Domain.Entities;

namespace LitterService.Persistence.Repositories
{
    public class FollowingRepository : Repository<LitterDbContext, Following>, IFollowingRepository
    {
        public FollowingRepository(LitterDbContext context) : base(context)
        {
        }
    }
}
