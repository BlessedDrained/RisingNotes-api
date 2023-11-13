using Dal.Author;
using Dal.BaseUser;
using Dal.Comment;
using Dal.File;
using Dal.Playlist;
using Dal.Song;
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

    /// <inheritdoc />
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetValue<string>("Dal:ConnectionString"));
        base.OnConfiguring(optionsBuilder);
    }

    ///
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("fuzzystrmatch");

        base.OnModelCreating(builder);

        builder.Entity<UserDal>();
        builder.Entity<SongDal>();
        builder.Entity<PlaylistDal>();
        builder.Entity<FileDal>();
        builder.Entity<CommentDal>();
        builder.Entity<AuthorDal>(config => config
            .HasMany(x => x.SongList)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId));

        builder.Entity<SongPublishRequestDal>();
    }
}