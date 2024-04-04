namespace Api.Controllers.User.Dto.Response;

/// <summary>
/// Ответ на получение инфы о пользователе
/// </summary>
public record GetUserResponse
{
    /// <summary>
    /// Имя пользователе
    /// </summary>
    public string UserName { get; init; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; init; }
}