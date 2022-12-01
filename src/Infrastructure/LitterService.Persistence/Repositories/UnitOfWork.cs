using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;

namespace LitterService.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LitterDbContext _context;

        public UnitOfWork(LitterDbContext context)
        {
            _context = context;
            Lits = new LitRepository(_context);
            Followings = new FollowingRepository(_context);
        }

        public ILitRepository Lits { get; private set; }
        public IFollowingRepository Followings { get; private set; }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}