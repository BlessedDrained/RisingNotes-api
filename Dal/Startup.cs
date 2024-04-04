using Amazon.S3;
using Dal.Author.Repository;
using Dal.BaseUser.Repository;
using Dal.Comment.Repository;
using Dal.Context;
using Dal.File.Repository;
using Dal.File.YandexDisk;
using Dal.Playlist.Repository;
using Dal.Song.Repository;
using Dal.SongPublish.Repository;

namespace Dal;

public static class Startup
{
    public static IServiceCollection AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>();

        services.AddTransient<ISongRepository, SongRepository>();
        services.AddTransient<IFileRepository, FileRepository>();
        services.AddTransient<IPlaylistRepository, PlaylistRepository>();
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISongPublishRequestRepository, SongPublishRequestRepository>();

        services.AddSingleton<IS3ClientFactory, S3ClientFactory>();
        
        return services;
    }
}