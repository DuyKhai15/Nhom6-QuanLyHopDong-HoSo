using Microsoft.EntityFrameworkCore;
using QLHopDongHoSo.API.Models;
using System.Reflection.Emit;

namespace QLHopDongHoSo.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Role
        modelBuilder.Entity<Role>()
            .HasKey(r => r.RoleId);

        modelBuilder.Entity<Role>()
            .Property(r => r.RoleName)
            .HasMaxLength(50)
            .IsRequired();

        // User
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.FullName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(100);

        // Role 1 - N User
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
    new Role
    {
        RoleId = 1,
        RoleName = "Admin"
    },
    new Role
    {
        RoleId = 2,
        RoleName = "Nhân viên"
    },
    new Role
    {
        RoleId = 3,
        RoleName = "Quản lý"
    }
);
    }
}