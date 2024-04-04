using Amazon.S3;

namespace Dal.File.YandexDisk;

/// <summary>
/// Фабрика клиентов S3
/// </summary>
public interface IS3ClientFactory
{
    /// <summary>
    /// Создать клиент
    /// </summary>
    IAmazonS3 CreateClient();
}