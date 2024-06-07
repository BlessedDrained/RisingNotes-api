﻿using Dal.Author.Repository;
using Dal.BaseUser.Repository;
using Dal.Context;
using Dal.File.Repository;
using Dal.File.YandexDisk;
using Dal.MusicClip.Repository;
using Dal.MusicClipComment.Repository;
using Dal.PersistedGrant.Repository;
using Dal.Playlist.Repository;
using Dal.ShortVideo.Repository;
using Dal.ShortVideoComment.Repository;
using Dal.Song.Repository;
using Dal.SongComment.Repository;
using Dal.SongPublish.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dal;

public static class Startup
{
    public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(config =>
        {
            config.UseNpgsql(configuration.GetValue<string>("Dal:ConnectionString"));
        });

        services.AddTransient<ISongRepository, SongRepository>();
        services.AddTransient<IFileRepository, FileRepository>();
        services.AddTransient<IPlaylistRepository, PlaylistRepository>();
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<ISongCommentRepository, SongCommentRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISongPublishRequestRepository, SongPublishRequestRepository>();
        services.AddSingleton<IYandexS3ClientFactory, YandexYandexS3ClientFactory>();
        services.AddTransient<IShortVideoRepository, ShortVideoRepository>();
        services.AddTransient<IShortVideoCommentRepository, ShortVideoCommentRepository>();
        services.AddTransient<IClipRepository, ClipRepository>();
        services.AddTransient<IClipCommentRepository, ClipCommentRepository>();
        services.AddTransient<IPersistedGrantRepository, PersistedGrantRepository>();
        
        return services;
    }
}