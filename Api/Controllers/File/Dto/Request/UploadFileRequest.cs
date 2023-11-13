namespace Api.Controllers.File.Dto.Request;

/// <summary>
/// Модель запроса на создание файла
/// </summary>
public record UploadFileRequest
{
    /// <summary>
    /// Файл
    /// </summary>
    public IFormFile File { get; init; }

    // Убрана, т.к пришел к тому, что это не фронтовая задача определять, куда загружать файл
    
    // /// <summary>
    // /// Тип хранилища
    // /// </summary>
    // public StorageType FileStorageType { get; init; }
}