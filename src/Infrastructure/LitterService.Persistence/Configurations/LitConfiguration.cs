using LitterService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LitterService.Persistence.Configurations
{
    public class LitConfiguation : IEntityTypeConfiguration<Lit>
    {
        public void Configure(EntityTypeBuilder<Lit> builder)
        {
        }
    }
}
