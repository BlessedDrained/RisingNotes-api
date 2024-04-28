using Logic.Author;
using Logic.File;
using Logic.Logo;
using Logic.MusicClip;
using Logic.MusicClipComment;
using Logic.Playlist;
using Logic.ShortVideo;
using Logic.ShortVideoComment;
using Logic.Song;
using Logic.SongComment;
using Logic.SongPublish;
using Logic.User;
using MainLib.Enums;

namespace Logic;

public static class Startup
{
    public static IServiceCollection AddLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISongManager, SongManager>();

        if (configuration.GetValue<StorageType>("FileSettings:StorageType") == StorageType.YandexDisk)
        {
            services.AddTransient<IFileManager, YandexFileManager>();
        }
        else
        {
            services.AddTransient<IFileManager, DbFileManager>();
        }
        
        services.AddTransient<IPlaylistManager, PlaylistManager>();
        services.AddTransient<IUserManager, UserManager>();
        services.AddTransient<ISongCommentManager, SongCommentManager>();
        services.AddTransient<ILogoResizeService, LogoResizeService>();
        services.AddTransient<IAuthorManager, AuthorManager>();
        services.AddTransient<ISongPublishManager, SongPublishManager>();
        services.AddTransient<IClipManager, ClipManager>();
        services.AddTransient<IClipCommentManager, ClipCommentManager>();
        services.AddTransient<IShortVideoManager, ShortVideoManager>();
        services.AddTransient<IShortVideoCommentManager, ShortVideoCommentManager>();

        return services;
    }
}