using Microsoft.EntityFrameworkCore;

namespace TreeStructureAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public virtual DbSet<Item> Items { get; set; }
}