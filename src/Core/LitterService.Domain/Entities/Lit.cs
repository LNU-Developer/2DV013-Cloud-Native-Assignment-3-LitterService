using System;
using LitterService.Domain.Common;

namespace LitterService.Domain.Entities
{
    public class Lit : AuditableEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public Guid CreatedByUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}