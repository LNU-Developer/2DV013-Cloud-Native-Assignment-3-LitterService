using LitterService.Application.Contracts.Persistence;
using LitterService.Domain.Entities;

namespace LitterService.Persistence.Repositories
{
    public class LitRepository : Repository<LitterDbContext, Lit>, ILitRepository
    {
        public LitRepository(LitterDbContext context) : base(context)
        {
        }
    }
}
