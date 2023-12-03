using Api.Controllers.File.Dto.Request;
using Api.Validation;
using RisingNotesLib.Enums;

namespace Api.Controllers.SongPublish.Dto.Request;

/// <summary>
/// Запрос на ответ на заявку от лица пользователя
/// </summary>
public record ReplyToRequestAsUserRequest
{
    /// <summary>
    /// Название песни
    /// </summary>
    public string SongName { get; init; }

    /// <summary>
    /// Текст песни
    /// </summary>
    public string Lyrics { get; init; }

    /// <summary>
    /// Список жанров песни
    /// </summary>
    public string[] GenreList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Список настроений
    /// </summary>
    public string[] VibeList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Список языков
    /// </summary>
    public string[] LanguageList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 
    /// </summary>
    public Gender[] VocalGenderList { get; set; } = Array.Empty<Gender>();
    
    /// <summary>
    /// Является ли инструментальной
    /// </summary>
    public bool Instrumental { get; set; }

    /// <summary>
    /// Файл песни
    /// </summary>
    [MaxFileSize(30720)]
    public UploadFileRequest SongFile { get; init; }

    /// <summary>
    /// Файл логотипа
    /// </summary>
    [MaxFileSize(2048)]
    public UploadFileRequest LogoFile { get; init; }
}