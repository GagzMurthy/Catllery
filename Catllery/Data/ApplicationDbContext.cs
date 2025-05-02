using Microsoft.EntityFrameworkCore;
using Catllery.Models;

namespace Catllery.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CatPhoto> CatPhotos { get; set; } = null!;
    }
}