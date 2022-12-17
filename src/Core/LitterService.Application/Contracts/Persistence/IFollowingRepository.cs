using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LitterService.Application.Features.Followings.Queries.GetFollowingsByUserId.Dtos;
using LitterService.Domain.Entities;
namespace LitterService.Application.Contracts.Persistence
{
    public interface IFollowingRepository : IRepository<Following>
    {
        Task<List<FollowingDto>> GetFollowingsByUserId(Guid id);
    }
}