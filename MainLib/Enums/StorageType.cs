namespace MainLib.Enums;

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
    /// S3-совместимое хранилище
    /// </summary>
    S3Storage = 2
}