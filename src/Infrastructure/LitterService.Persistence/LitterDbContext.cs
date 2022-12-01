using LitterService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LitterService.Persistence
{
    public partial class LitterDbContext : DbContext
    {
        public LitterDbContext(DbContextOptions<LitterDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lit> Lits { get; set; }
        public virtual DbSet<Following> Followings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LitterDbContext).Assembly);
        }
    }
}
