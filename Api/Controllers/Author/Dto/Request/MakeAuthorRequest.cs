using System.ComponentModel.DataAnnotations;

namespace Api.Controllers.Author.Dto.Request;

/// <summary>
/// Запрос на становление музыкантом
/// </summary>
public record MakeAuthorRequest
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Required]
    public Guid UserId { get; init; }
    
    /// <summary>
    /// Имя автора
    /// </summary>
    [Required]
    public string Name { get; init; }
    
    /// <summary>
    /// Блок "О себе"
    /// </summary>
    public string About { get; set; }
    
    /// <summary>
    /// Ссылка на Vk
    /// </summary>
    public string VkLink { get; set; }
    
    /// <summary>
    /// Ссылка на яндекс музыку
    /// </summary>
    public string YaMusicLink { get; set; }
    
    /// <summary>
    /// Ссылка на личный сайт
    /// </summary>
    public string WebSiteLink { get; set; }
}