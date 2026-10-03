using Microsoft.EntityFrameworkCore;
using QLHopDongHoSo.API.Models;

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

    public DbSet<Contract> Contracts => Set<Contract>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =====================================================
        // Role
        // =====================================================

        modelBuilder.Entity<Role>()
            .HasKey(r => r.RoleId);

        modelBuilder.Entity<Role>()
            .Property(r => r.RoleName)
            .HasMaxLength(50)
            .IsRequired();

        // =====================================================
        // User
        // =====================================================

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

        // =====================================================
        // Contract
        // =====================================================

        modelBuilder.Entity<Contract>()
            .HasKey(c => c.ContractId);

        modelBuilder.Entity<Contract>()
            .Property(c => c.ContractCode)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Contract>()
            .Property(c => c.ContractName)
            .HasMaxLength(200)
            .IsRequired();

        modelBuilder.Entity<Contract>()
            .Property(c => c.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<Contract>()
            .Property(c => c.Status)
            .HasMaxLength(50)
            .IsRequired();

        // User 1 - N Contract
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.Creator)
            .WithMany()
            .HasForeignKey(c => c.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        // =====================================================
        // Seed Roles
        // =====================================================

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