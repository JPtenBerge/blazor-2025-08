using DemoProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.DataAccess;

public class DemoContext : DbContext
{
    public DbSet<Destination> Destinations { get; set; }

    public DemoContext(DbContextOptions<DemoContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>().HasKey(x => x.Id);
        modelBuilder.Entity<Destination>().Property(x => x.Location).IsRequired().HasMaxLength(150);
        modelBuilder.Entity<Destination>().Property(x => x.Rating).IsRequired();
        modelBuilder.Entity<Destination>().Property(x => x.PhotoUrl).IsRequired().HasMaxLength(400);
        modelBuilder.Entity<Destination>().Ignore(x => x.Description);

    }
}
