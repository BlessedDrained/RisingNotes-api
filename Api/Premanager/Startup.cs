using Api.Premanager.Auth;
using Api.Premanager.Author;
using Api.Premanager.Clip;
using Api.Premanager.ClipComment;
using Api.Premanager.Music;
using Api.Premanager.Playlist;
using Api.Premanager.ShortVideo;
using Api.Premanager.ShortVideoComment;
using Api.Premanager.SongComment;
using Api.Premanager.SongPublish;
using Api.Premanager.User;

namespace Api.Premanager;

/// <summary>
/// 
/// </summary>
public static class Startup
{
    /// <summary>
    /// Добавить premanagers
    /// </summary>
    public static IServiceCollection AddPremanagers(this IServiceCollection services)
    {
        services.AddTransient<ISongPremanager, SongPremanager>();
        services.AddTransient<IPlaylistPremanager, PlaylistPremanager>();
        services.AddTransient<IProfilePremanager, ProfilePremanager>();
        services.AddTransient<ISongCommentPremanager, SongCommentPremanager>();
        services.AddTransient<IUserPremanager, UserPremanager>();
        services.AddTransient<IAuthorPremanager, AuthorPremanager>();
        services.AddTransient<ISongPublishPremanager, SongPublishPremanager>();
        services.AddTransient<IClipPremanager, ClipPremanager>();
        services.AddTransient<IShortVideoPremanager, ShortVideoPremanager>();
        services.AddTransient<IClipCommentPremanager, ClipCommentPremanager>();
        services.AddTransient<IShortVideoCommentPremanager, IShortVideoCommentPremanager>();
        
        return services;
    }
}