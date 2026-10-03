using Microsoft.EntityFrameworkCore;
using QLHopDongHoSo.API.Models;

namespace QLHopDongHoSo.API.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(
        ApplicationDbContext context)
    {
        // Đảm bảo database đã được migrate
        await context.Database.MigrateAsync();

        // Nếu đã có User thì không tạo lại
        if (await context.Users.AnyAsync())
        {
            return;
        }

        // Lấy Role Admin
        var adminRole = await context.Roles
            .FirstAsync(r => r.RoleName == "Admin");

        // Tạo Admin
        var admin = new User
        {
            Username = "admin",

            PasswordHash =
                BCrypt.Net.BCrypt.HashPassword("Admin@123"),

            FullName = "System Administrator",

            Email = "admin@example.com",

            RoleId = adminRole.RoleId,

            IsActive = true,

            CreatedAt = DateTime.UtcNow
        };

        context.Users.Add(admin);

        await context.SaveChangesAsync();
    }
}