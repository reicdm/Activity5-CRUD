using Activity5_CRUD.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Activity5_CRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Art> Arts { get; set; }
    }
}
