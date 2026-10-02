using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QLHopDongHoSo.API.Data;
using QLHopDongHoSo.API.DTOs;

namespace QLHopDongHoSo.API.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(
        ApplicationDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        // 1. Tìm user đang hoạt động
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u =>
                u.Username == request.Username &&
                u.IsActive);

        if (user == null)
        {
            return null;
        }

        // 2. Kiểm tra password
        var passwordValid = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!passwordValid)
        {
            return null;
        }

        // 3. Tạo JWT
        var token = GenerateJwtToken(
            user.UserId,
            user.Username,
            user.Role!.RoleName);

        // 4. Trả kết quả
        return new LoginResponse
        {
            Token = token,
            UserId = user.UserId,
            Username = user.Username,
            FullName = user.FullName,
            Role = user.Role.RoleName
        };
    }

    private string GenerateJwtToken(
        int userId,
        string username,
        string role)
    {
        var jwtKey = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException(
                "JWT Key chưa được cấu hình.");
        }

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var duration = _configuration.GetValue<int>(
            "Jwt:DurationInMinutes");

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.ToString()),

            new Claim(
                ClaimTypes.Name,
                username),

            new Claim(
                ClaimTypes.Role,
                role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(duration),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}