using Dal.File.Enums;
using Destructurama.Attributed;
using MainLib.Dal.Model.Base;

namespace Dal.File;

/// <summary>
/// Модель файла
/// </summary>
public record FileDal : DalModel<Guid>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Расширение файла
    /// </summary>
    public string Extension { get; set; }

    /// <summary>
    /// Тип хранилища
    /// </summary>
    public StorageType StorageType { get; set; }

    /// <summary>
    /// Содержимое файла
    /// </summary>
    [LogMasked(Text = "BINARY CONTENT")]
    public byte[] Content { get; set; }
}