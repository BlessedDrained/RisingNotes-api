using Api.Controllers.Profile.Dto.Request;
using Api.Controllers.Profile.Dto.Response;

namespace Api.Premanager.Auth;

/// <summary>
/// Premanager для регистрации
/// </summary>
public interface IProfilePremanager
{
    /// <summary>
    /// Зарегистрировать пользователя
    /// </summary>
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);

    /// <summary>
    /// Поменять роль
    /// </summary>
    Task ChangeRoleAsync(string userId, string newRoleName);
    
    /// <summary>
    /// Получить информацию профиля пользователя
    /// </summary>
    Task<GetProfileResponse> GetProfileAsync(Guid identityUserId);
}