using System.ComponentModel.DataAnnotations;
using MainLib.Api.Validation;

namespace Api.Controllers.Clip.Dto.Request;

/// <summary>
/// Запрос на загрузку части файла с клипом
/// </summary>
public record UploadClipFilePartRequest
{
    /// <summary>
    /// Идентификатор загрузки
    /// </summary>
    [Required]
    public string UploadId { get; init; }
    
    /// <summary>
    /// Файл
    /// </summary>
    [MinFileSize(5 * 1024 * 1024)]
    [Required]
    public IFormFile File { get; init; }
    
    /// <summary>
    /// Порядковый номер части файла
    /// </summary>
    public int PartNumber { get; init; }
    
    /// <summary>
    /// Является ли переданная часть файла последней
    /// </summary>
    public bool IsLastPart { get; init; }
}