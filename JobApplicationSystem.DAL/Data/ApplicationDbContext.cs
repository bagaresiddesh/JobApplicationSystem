using JobApplicationSystem.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationSystem.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<AddressDetails> AddressDetails { get; set; }
        public DbSet<EducationalDetails> EducationalDetails { get; set; }
    }
}
