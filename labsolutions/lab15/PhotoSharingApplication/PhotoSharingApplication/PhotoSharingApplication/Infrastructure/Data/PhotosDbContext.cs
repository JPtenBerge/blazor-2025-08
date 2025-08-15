using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Core.Models;

namespace PhotoSharingApplication.Infrastructure.Data;

public class PhotosDbContext : DbContext
{
    public PhotosDbContext(DbContextOptions<PhotosDbContext> options) : base(options)
    {
        
    }

    public DbSet<Photo> Photos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Photo>().Property(p => p.Title).HasMaxLength(100);
        modelBuilder.Entity<Photo>().Property(p => p.Description).HasMaxLength(300);
        modelBuilder.Entity<Photo>().Property(p => p.ImageMimeType).HasMaxLength(100);
    }
}
