
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LitterService.Application.Contracts.Persistence;
using LitterService.Application.Features.Lits.Queries.GetLitsByUserId;
using LitterService.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace LitterService.Application.UnitTests.Features.Lits.Queries
{

    public class GetLitsByUserIdQueryHandlerTests
    {
        public GetLitsByUserIdQueryHandlerTests()
        {
        }
        [Fact]
        public async Task Handle_Lits_ShouldNotContainDeletedItems()
        {
            //Arrange
            var query = new GetLitsByUserIdQuery(Guid.NewGuid());
            var unitOfWork = new Mock<IUnitOfWork>();
            var lits = new List<Lit>
            {
                new Lit{Id = 1, Message = "test", CreatedByUserId = Guid.NewGuid(), IsDeleted = true},
                new Lit{Id = 2, Message = "test", CreatedByUserId = Guid.NewGuid(), IsDeleted = false},
            };
            var sut = new GetLitsByUserIdQueryHandler(unitOfWork.Object);
            unitOfWork.Setup(uow => uow.Lits.FindAsync(It.IsAny<Expression<Func<Lit, bool>>>()))
                    .ReturnsAsync(lits);

            //Act
            var result = await sut.Handle(query, new CancellationToken());

            //Assert
            unitOfWork.Verify(x => x.Lits.FindAsync(It.IsAny<Expression<Func<Lit, bool>>>()), Times.Once());
            result.Count.ShouldBe(lits.Count(x => x.IsDeleted == true));
        }
    }
}