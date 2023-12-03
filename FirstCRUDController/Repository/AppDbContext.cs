using FirstCRUDController.Entities;
using FirstCRUDController.Entities.Maids;
using FirstCRUDController.Entities.Rooms;
using Microsoft.EntityFrameworkCore;

namespace FirstCRUDController.Repository;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }
    
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Maid> Maids { get; set; }
}