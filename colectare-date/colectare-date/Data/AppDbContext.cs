using colectare_date.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace colectare_date.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cetatean> Cetateni { get; set; }
        public DbSet<Pubela> Pubele { get; set; }
        public DbSet<PubelaCetateni> PubeleCetateni { get; set; }
        public DbSet<Colectare> Colectari { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PubelaCetateni>()
                .HasOne(pc => pc.Cetatean)
                .WithMany()
                .HasForeignKey(pc => pc.CetateanId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PubelaCetateni>()
                .HasOne(pc => pc.Pubela)
                .WithMany()
                .HasForeignKey(pc => pc.PubelaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
