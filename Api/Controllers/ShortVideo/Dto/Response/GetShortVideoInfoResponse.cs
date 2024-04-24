namespace Api.Controllers.ShortVideo.Dto.Response;

/// <summary>
/// Ответ на получение инфы о клипе
/// </summary>
public record GetShortVideoInfoResponse
{
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// Идентификатор файла превью
    /// </summary>
    public Guid PreviewFileId { get; init; }
    
    /// <summary>
    /// Идентификатор файла клипа
    /// </summary>
    public Guid VideoFileId { get; init; }
    
    /// <summary>
    /// Идентификатор песни, для которой создан клип
    /// </summary>
    public Guid? RelatedSongId { get; init; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; init; }
    
    /// <summary>
    /// Идентификатор кто загрузил клип
    /// </summary>
    public Guid UploaderId { get; init; }
}