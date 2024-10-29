using Dal.Author;
using Dal.BaseUser;
using Dal.File;
using Dal.MusicClip;
using Dal.MusicClipComment;
using Dal.PersistedGrant;
using Dal.Playlist;
using Dal.ShortVideo;
using Dal.ShortVideoComment;
using Dal.Song;
using Dal.SongComment;
using Dal.SongPublish;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RisingNotesLib.Models;

namespace Dal.Context;

/// <summary>
/// Контекст приложения
/// </summary>
public class ApplicationContext : IdentityDbContext<AppIdentityUser>
{
    private readonly IConfiguration _configuration;

    /// <inheritdoc/>
    public ApplicationContext() : base()
    {
        
    }
    
    /// <inheritdoc />
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetValue<string>("Dal:ConnectionString"), config => config.EnableRetryOnFailure(3));
        base.OnConfiguring(optionsBuilder);
    }

    ///
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PersistedGrantDal>();
        builder.Entity<UserDal>();
        builder.Entity<SongDal>();
        builder.Entity<PlaylistDal>();
        builder.Entity<FileDal>();
        builder.Entity<SongCommentDal>();
        builder.Entity<AuthorDal>(config => config
            .HasMany(x => x.SongList)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId));

        builder.Entity<AuthorDal>()
            .HasOne(x => x.User)
            .WithOne();
        
        builder.Entity<UserDal>()
            .HasMany(x => x.SubscriptionList)
            .WithMany(x => x.SubscribedUserList);

        builder.Entity<UserDal>()
            .HasMany(x => x.ExcludedSongList)
            .WithMany(x => x.ExcludedUserList);

        builder.Entity<UserDal>()
            .HasMany(x => x.FavoriteSongList)
            .WithMany(x => x.AddedToFavoriteUserList);
        
        builder.Entity<SongPublishRequestDal>();

        builder.Entity<ClipCommentDal>();
        builder.Entity<ShortVideoCommentDal>();

        builder.Entity<ClipDal>();

        builder.Entity<ShortVideoDal>();
    }
}