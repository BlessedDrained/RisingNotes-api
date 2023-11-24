using MainLib.Filters;
using RisingNotesLib.Enums;

namespace RisingNotesLib.Models;

public record GetSongListFilterModel
{
    /// <summary>
    /// Список жанров
    /// </summary>
    public CollectionFieldFilter<string> GenreList { get; init; } = new();

    /// <summary>
    /// Список языков
    /// </summary>
    public CollectionFieldFilter<string> LanguageList { get; init; } = new();

    /// <summary>
    /// Список настроений
    /// </summary>
    public CollectionFieldFilter<string> VibeList { get; init; } = new();
    
    /// <summary>
    /// Пол
    /// </summary>
    public Gender? Gender { get; init; }
    
    /// <summary>
    /// Продолжительность трека
    /// </summary>
    public DoubleRange? TrackDurationRange { get; init; }
    
    /// <summary>
    /// Является ли инструментальной
    /// </summary>
    public bool? Instrumental { get; init; }
}