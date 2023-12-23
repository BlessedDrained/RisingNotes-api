namespace Api.Controllers.Author.Dto.Response;

/// <summary>
/// Ответ на получение информаици об авторе
/// </summary>
public record GetAuthorInfoResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Имя автора
    /// </summary>
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
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
}