using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
    public DbSet<UserDislike> Dislikes { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserDislike>()
            .HasKey(k => new { k.SourceUserId, k.TargetUserId });

        builder.Entity<UserDislike>()
            .HasOne(s => s.SourceUser)
            .WithMany(l => l.DislikedUsers)
            .HasForeignKey(s => s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserDislike>()
            .HasOne(s => s.TargetUser)
            .WithMany(l => l.DislikedByUsers)
            .HasForeignKey(s => s.TargetUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}