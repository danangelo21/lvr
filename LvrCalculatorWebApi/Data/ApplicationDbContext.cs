using Microsoft.EntityFrameworkCore;
using LvrCalculatorWebApi.Models;

namespace LvrCalculatorWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LvrEntry> LvrEntries { get; set; }
    }
}
