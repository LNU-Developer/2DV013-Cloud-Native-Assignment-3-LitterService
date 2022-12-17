using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId.Dtos;
using LitterService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LitterService.Persistence.Repositories
{
    public class FollowingRepository : Repository<LitterDbContext, Following>, IFollowingRepository
    {
        public FollowingRepository(LitterDbContext context) : base(context)
        {
        }

        public async Task<List<FollowingDto>> GetFollowingsByUserId(Guid id)
        {
            return await _context.Followings
                .Where(x => x.FollowingUserId == id && x.IsDeleted == false)
                .Select(user => new FollowingDto(user.FollowingUserId, user.FollowedUserId, user.CreatedAt, user.UpdatedAt))
                .ToListAsync();
        }
    }
}
