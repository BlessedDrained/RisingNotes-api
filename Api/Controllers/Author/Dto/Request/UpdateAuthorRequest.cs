namespace Api.Controllers.Author.Dto.Request;

/// <summary>
/// Запрос на обновление автора
/// </summary>
public record UpdateAuthorRequest
{
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