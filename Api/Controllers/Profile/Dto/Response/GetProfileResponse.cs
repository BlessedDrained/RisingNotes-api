namespace Api.Controllers.Profile.Dto.Response;

/// <summary>
/// Ответ на получение информации о профиле пользователя
/// </summary>
public record GetProfileResponse
{
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; init; }
}