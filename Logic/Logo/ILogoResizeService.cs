using Dal.File;

namespace Logic.Logo;

/// <summary>
/// Сервис для работы с лого
/// </summary>
public interface ILogoResizeService
{
    /// <summary>
    /// Получить обработанное лого
    /// </summary>
    Task<byte[]> ResizeAsync(FileDal fileDal, int? width, int? height);
}