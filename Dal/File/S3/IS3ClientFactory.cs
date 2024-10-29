namespace Dal.File.S3;

/// <summary>
/// Фабрика клиентов S3
/// </summary>
public interface IS3ClientFactory
{
    /// <summary>
    /// Создать клиент
    /// </summary>
    S3Client CreateClient();
}