
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Application.Features.Lits.Queries.GetOwnAndFollowedLits;
using LitterService.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace LitterService.Application.UnitTests.Features.Lits.Queries
{

    public class GetOwnAndFollowedLitsQueryHandlerTests
    {
        public GetOwnAndFollowedLitsQueryHandlerTests()
        {
        }
        [Fact]
        public async Task Handle_Lits_ShouldOnlyContainActiveOwnAndFollowingItems()
        {
            //Arrange
            var followedId = Guid.NewGuid();
            var followingId = Guid.NewGuid();
            var followings = new List<Following>()
            {
                new Following{Id = 1, FollowingUserId = followingId, FollowedUserId = followedId, IsDeleted = false},
                new Following{Id = 2, FollowingUserId = followingId, FollowedUserId = followedId, IsDeleted = true},
            };

            var query = new GetOwnAndFollowedLitsQuery(followingId);
            var unitOfWork = new Mock<IUnitOfWork>();
            var lits = new List<Lit>
            {
                new Lit{Id = 1, Message = "test", CreatedByUserId = followedId, IsDeleted = false},
                new Lit{Id = 2, Message = "test", CreatedByUserId = followingId, IsDeleted = false},
                new Lit{Id = 2, Message = "test", CreatedByUserId = followingId, IsDeleted = true},
                new Lit{Id = 3, Message = "test", CreatedByUserId = Guid.NewGuid(), IsDeleted = false}
            };
            var sut = new GetOwnAndFollowedLitsQueryHandler(unitOfWork.Object);
            unitOfWork.Setup(uow => uow.Followings.FindAsync(It.IsAny<Expression<Func<Following, bool>>>()))
                    .ReturnsAsync(followings);
            unitOfWork.Setup(uow => uow.Lits.FindAsync(It.IsAny<Expression<Func<Lit, bool>>>()))
                    .ReturnsAsync(lits);

            //Act
            var result = await sut.Handle(query, new CancellationToken());

            //Assert
            unitOfWork.Verify(x => x.Lits.FindAsync(It.IsAny<Expression<Func<Lit, bool>>>()), Times.Exactly(3)); // Twice for following and once for own
            unitOfWork.Verify(x => x.Followings.FindAsync(It.IsAny<Expression<Func<Following, bool>>>()), Times.Once());
            result.Count.ShouldBe(lits.Count(x => x.IsDeleted == true && x.CreatedByUserId == followedId || x.CreatedByUserId == followingId));
        }
    }
}