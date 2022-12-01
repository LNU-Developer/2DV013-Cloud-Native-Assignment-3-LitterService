using System;
using LitterService.Domain.Common;

namespace LitterService.Domain.Entities
{
    public class Following : AuditableEntity
    {
        public int Id { get; set; }
        public Guid FollowingUserId { get; set; }
        public Guid FollowedUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}