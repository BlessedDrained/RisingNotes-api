using Logic.Author;
using Logic.File;
using Logic.Logo;
using Logic.Playlist;
using Logic.Song;
using Logic.SongComment;
using Logic.SongPublish;
using Logic.User;

namespace Logic;

public static class Startup
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services)
    {
        services.AddTransient<ISongManager, SongManager>();
        services.AddTransient<IFileManager, DbFileManager>();
        services.AddTransient<IPlaylistManager, PlaylistManager>();
        services.AddTransient<IUserManager, UserManager>();
        services.AddTransient<ISongCommentManager, SongCommentManager>();
        services.AddTransient<ILogoResizeService, LogoResizeService>();
        services.AddTransient<IAuthorManager, AuthorManager>();
        services.AddTransient<ISongPublishManager, SongPublishManager>();

        return services;
    }
}