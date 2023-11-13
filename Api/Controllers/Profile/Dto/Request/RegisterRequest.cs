using Destructurama.Attributed;

namespace Api.Controllers.Profile.Dto.Request;

/// <summary>
/// Модель запроса на регистрацию
/// </summary>
public record RegisterRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; init; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    [LogMasked(Text = "***")]
    public string Password { get; init; }
}