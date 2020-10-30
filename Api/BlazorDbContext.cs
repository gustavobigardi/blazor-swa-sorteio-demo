using BlazorSwa.Shared;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class BlazorDbContext : DbContext
    {
        public BlazorDbContext(DbContextOptions<BlazorDbContext> options) : base(options)
        { 
            
        }

        public DbSet<User> Users { get; set; }
    }
}