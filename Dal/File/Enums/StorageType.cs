namespace Dal.File.Enums;

/// <summary>
/// Тип хранилища для файла
/// </summary>
public enum StorageType
{
    /// 
    Unknown = 0,
    
    /// <summary>
    /// БД
    /// </summary>
    Database = 1,
    
    /// <summary>
    /// Внутри контейнера
    /// </summary>
    Container = 2,
    
    /// <summary>
    /// Яндекс диск
    /// </summary>
    YandexDisk = 3
}