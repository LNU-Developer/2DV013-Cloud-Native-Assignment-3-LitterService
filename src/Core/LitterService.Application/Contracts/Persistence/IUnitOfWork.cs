using System;
using System.Threading.Tasks;

namespace LitterService.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ILitRepository Lits { get; }
        IFollowingRepository Followings { get; }

        Task<int> CompleteAsync();
        int Complete();
    }
}