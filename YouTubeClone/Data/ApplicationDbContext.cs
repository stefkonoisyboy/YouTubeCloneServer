using Microsoft.EntityFrameworkCore;
using YouTubeClone.Data.Models;

namespace YouTubeClone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseSqlServer("Server=.;Database=YouTubeClone;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public virtual DbSet<ApplicationUser> Users { get; set; }
    }
}
