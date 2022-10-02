using Microsoft.EntityFrameworkCore;
using NotificationTest.Entities;

namespace Web.Repositories;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Gift>? Gifts { get; set; }
    public DbSet<Item>? Items { get; set; }
    public DbSet<Player>? Players { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

}
