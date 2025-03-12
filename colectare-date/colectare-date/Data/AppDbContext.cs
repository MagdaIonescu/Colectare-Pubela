using colectare_date.Models;
using Microsoft.EntityFrameworkCore;

namespace colectare_date.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Colectare> Colectari { get; set; }
    }
}
