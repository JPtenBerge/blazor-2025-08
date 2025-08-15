using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.CommentsApi.Core.Models;

namespace PhotoSharingApplication.CommentsApi.Infrastructure.Data;

public class CommentsDbContext : DbContext
{
    public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options)
    {

    }

    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().Property(c => c.Subject).HasMaxLength(100);
        modelBuilder.Entity<Comment>().Property(c => c.Body).HasMaxLength(300);
    }
}
