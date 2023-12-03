using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.User.Dto.Request;

/// <summary>
/// Обновить информацию о пользователе
/// </summary>
public record UpdateUserNameRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public string UserName { get; set; }
}