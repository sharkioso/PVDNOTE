using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Core.Entities;


namespace PVDNOTE.Backend.Infrastructure.Data;

public class DBContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<WorkSpace> WorkSpaces { get; set; }
    public virtual DbSet<UserWorkSpace> UserWorkSpaces { get; set; }
    public virtual DbSet<Pages> Pages { get; set; }
    public virtual DbSet<Block> Blocks { get; set; }

    public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

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


        modelBuilder.Entity<Pages>()
            .HasOne(p => p.WorkSpace)
            .WithMany(w => w.Pages)
            .HasForeignKey(p => p.WorkSpaceId);

        modelBuilder.Entity<Block>()
            .HasOne(b => b.Page)
            .WithMany(p => p.Blocks)
            .HasForeignKey(b => b.PageId);
    }

}