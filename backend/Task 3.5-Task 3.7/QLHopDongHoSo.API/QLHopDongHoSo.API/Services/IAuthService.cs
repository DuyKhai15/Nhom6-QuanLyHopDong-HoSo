using QLHopDongHoSo.API.DTOs;

namespace QLHopDongHoSo.API.Services;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);

    Task<object?> RegisterAsync(RegisterRequest request);
}