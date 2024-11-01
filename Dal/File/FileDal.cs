﻿using Destructurama.Attributed;
using MainLib.Dal.Model.Base;
using MainLib.Enums;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Helper;

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

public static class FileDalExtensions
{
    /// <summary>
    /// Получить FileContentResult из <see cref="FileDal"/>
    /// </summary>
    public static FileContentResult ToFileContent(this FileDal file)
    {
        return new FileContentResult(file.Content, ContentTypeHelper.GetContentTypeByFileExtension(file.Extension));
    }
}