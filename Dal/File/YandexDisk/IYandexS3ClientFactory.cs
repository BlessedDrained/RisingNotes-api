using Amazon.S3;

namespace Dal.File.YandexDisk;

/// <summary>
/// Фабрика клиентов S3
/// </summary>
public interface IYandexS3ClientFactory
{
    /// <summary>
    /// Создать клиент
    /// </summary>
    YandexS3Client CreateClient();
}