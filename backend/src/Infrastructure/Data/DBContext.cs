using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;


namespace PVDNOTE.Backend.Infrastructure.Data;

public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<WorkSpace> WorkSpaces { get; set; }
    public DbSet<UserWorkSpace> UserWorkSpaces { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Block> Blocks { get; set; }

    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserWorkSpace>()
            .HasKey(uw => new { uw.UserId, uw.WorkSpaceId });

        modelBuilder.Entity<UserWorkSpace>()
            .HasOne(uw => uw.User)
            .WithMany(uw => uw.UserWorkSpaces)
            .HasForeignKey(uw => uw.UserId);

        modelBuilder.Entity<UserWorkSpace>()
            .HasOne(uw => uw.WorkSpace)
            .WithMany(uw => uw.UserWorkSpaces)
            .HasForeignKey(uw => uw.WorkSpaceId);


        modelBuilder.Entity<Page>()
            .HasOne(p => p.WorkSpace)
            .WithMany(p => p.Pages)
            .HasForeignKey(p => p.WorkSpaceId);
        
    }

}