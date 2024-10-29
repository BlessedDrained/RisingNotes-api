﻿// <auto-generated />
using System;
using Dal.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dal.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AuthorDalUserDal", b =>
                {
                    b.Property<Guid>("SubscribedUserListId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubscriptionListId")
                        .HasColumnType("uuid");

                    b.HasKey("SubscribedUserListId", "SubscriptionListId");

                    b.HasIndex("SubscriptionListId");

                    b.ToTable("AuthorDalUserDal");
                });

            modelBuilder.Entity("Dal.Author.AuthorDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("About")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("VkLink")
                        .HasColumnType("text");

                    b.Property<string>("WebSiteLink")
                        .HasColumnType("text");

                    b.Property<string>("YaMusicLink")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("AuthorDal");
                });

            modelBuilder.Entity("Dal.BaseUser.UserDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("text");

                    b.Property<bool>("IsAuthor")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LogoFileId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId");

                    b.HasIndex("LogoFileId");

                    b.ToTable("UserDal");
                });

            modelBuilder.Entity("Dal.File.FileDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Content")
                        .HasColumnType("bytea");

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("StorageType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("FileDal");
                });

            modelBuilder.Entity("Dal.MusicClip.ClipDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClipFileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DurationMsec")
                        .HasColumnType("integer");

                    b.Property<Guid>("PreviewFileId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UploaderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UploaderId");

                    b.ToTable("ClipDal");
                });

            modelBuilder.Entity("Dal.MusicClipComment.ClipCommentDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("ClipCommentDal");
                });

            modelBuilder.Entity("Dal.PersistedGrant.PersistedGrantDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ClientId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("SessionId")
                        .HasColumnType("text");

                    b.Property<string>("SubjectId")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PersistedGrantDal");
                });

            modelBuilder.Entity("Dal.Playlist.PlaylistDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LogoFileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LogoFileId");

                    b.ToTable("PlaylistDal");
                });

            modelBuilder.Entity("Dal.ShortVideo.ShortVideoDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DurationMsec")
                        .HasColumnType("integer");

                    b.Property<Guid>("PreviewFileId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RelatedSongId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("UploaderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VideoFileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UploaderId");

                    b.ToTable("ShortVideoDal");
                });

            modelBuilder.Entity("Dal.ShortVideoComment.ShortVideoCommentDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ShortVideoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("ShortVideoCommentDal");
                });

            modelBuilder.Entity("Dal.Song.SongDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AuditionCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<int>("DurationMsec")
                        .HasColumnType("integer");

                    b.Property<string[]>("GenreList")
                        .HasColumnType("text[]");

                    b.Property<bool>("Instrumental")
                        .HasColumnType("boolean");

                    b.Property<string[]>("LanguageList")
                        .HasColumnType("text[]");

                    b.Property<Guid?>("LogoFileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Lyrics")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("SongFileId")
                        .HasColumnType("uuid");

                    b.Property<string[]>("VibeList")
                        .HasColumnType("text[]");

                    b.Property<int[]>("VocalGenderList")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("LogoFileId");

                    b.HasIndex("SongFileId");

                    b.ToTable("SongDal");
                });

            modelBuilder.Entity("Dal.SongComment.SongCommentDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("SongId");

                    b.ToTable("SongCommentDal");
                });

            modelBuilder.Entity("Dal.SongPublish.SongPublishRequestDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<int>("DurationMs")
                        .HasColumnType("integer");

                    b.Property<string[]>("GenreList")
                        .HasColumnType("text[]");

                    b.Property<bool>("Instrumental")
                        .HasColumnType("boolean");

                    b.Property<string[]>("LanguageList")
                        .HasColumnType("text[]");

                    b.Property<Guid?>("LogoFileId")
                        .HasColumnType("uuid");

                    b.Property<string>("Lyrics")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ReviewerComment")
                        .HasColumnType("text");

                    b.Property<Guid?>("SongFileId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SongId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string[]>("VibeList")
                        .HasColumnType("text[]");

                    b.Property<int[]>("VocalGenderList")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("LogoFileId");

                    b.HasIndex("SongFileId");

                    b.HasIndex("SongId");

                    b.ToTable("SongPublishRequestDal");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PlaylistDalSongDal", b =>
                {
                    b.Property<Guid>("PlaylistListId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongListId")
                        .HasColumnType("uuid");

                    b.HasKey("PlaylistListId", "SongListId");

                    b.HasIndex("SongListId");

                    b.ToTable("PlaylistDalSongDal");
                });

            modelBuilder.Entity("RisingNotesLib.Models.AppIdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("SongDalUserDal", b =>
                {
                    b.Property<Guid>("ExcludedSongListId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExcludedUserListId")
                        .HasColumnType("uuid");

                    b.HasKey("ExcludedSongListId", "ExcludedUserListId");

                    b.HasIndex("ExcludedUserListId");

                    b.ToTable("SongDalUserDal");
                });

            modelBuilder.Entity("SongDalUserDal1", b =>
                {
                    b.Property<Guid>("AddedToFavoriteUserListId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FavoriteSongListId")
                        .HasColumnType("uuid");

                    b.HasKey("AddedToFavoriteUserListId", "FavoriteSongListId");

                    b.HasIndex("FavoriteSongListId");

                    b.ToTable("SongDalUserDal1");
                });

            modelBuilder.Entity("AuthorDalUserDal", b =>
                {
                    b.HasOne("Dal.BaseUser.UserDal", null)
                        .WithMany()
                        .HasForeignKey("SubscribedUserListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.Author.AuthorDal", null)
                        .WithMany()
                        .HasForeignKey("SubscriptionListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Author.AuthorDal", b =>
                {
                    b.HasOne("Dal.BaseUser.UserDal", "User")
                        .WithOne()
                        .HasForeignKey("Dal.Author.AuthorDal", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dal.BaseUser.UserDal", b =>
                {
                    b.HasOne("RisingNotesLib.Models.AppIdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");

                    b.HasOne("Dal.File.FileDal", "LogoFile")
                        .WithMany()
                        .HasForeignKey("LogoFileId");

                    b.Navigation("IdentityUser");

                    b.Navigation("LogoFile");
                });

            modelBuilder.Entity("Dal.MusicClip.ClipDal", b =>
                {
                    b.HasOne("Dal.Author.AuthorDal", "Uploader")
                        .WithMany("MusicClipList")
                        .HasForeignKey("UploaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("Dal.MusicClipComment.ClipCommentDal", b =>
                {
                    b.HasOne("Dal.BaseUser.UserDal", "Creator")
                        .WithMany("MusicClipCommentList")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Dal.Playlist.PlaylistDal", b =>
                {
                    b.HasOne("Dal.BaseUser.UserDal", "Creator")
                        .WithMany("PlaylistList")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.File.FileDal", "LogoFile")
                        .WithMany()
                        .HasForeignKey("LogoFileId");

                    b.Navigation("Creator");

                    b.Navigation("LogoFile");
                });

            modelBuilder.Entity("Dal.ShortVideo.ShortVideoDal", b =>
                {
                    b.HasOne("Dal.Author.AuthorDal", "Uploader")
                        .WithMany("ShortVideoList")
                        .HasForeignKey("UploaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uploader");
                });

            modelBuilder.Entity("Dal.ShortVideoComment.ShortVideoCommentDal", b =>
                {
                    b.HasOne("Dal.BaseUser.UserDal", "Creator")
                        .WithMany("ShortVideoCommentList")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Dal.Song.SongDal", b =>
                {
                    b.HasOne("Dal.Author.AuthorDal", "Author")
                        .WithMany("SongList")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.File.FileDal", "LogoFile")
                        .WithMany()
                        .HasForeignKey("LogoFileId");

                    b.HasOne("Dal.File.FileDal", "SongFile")
                        .WithMany()
                        .HasForeignKey("SongFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("LogoFile");

                    b.Navigation("SongFile");
                });

            modelBuilder.Entity("Dal.SongComment.SongCommentDal", b =>
                {
                    b.HasOne("Dal.BaseUser.UserDal", "Creator")
                        .WithMany("SongCommentList")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.Song.SongDal", "Song")
                        .WithMany("CommentList")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Dal.SongPublish.SongPublishRequestDal", b =>
                {
                    b.HasOne("Dal.Author.AuthorDal", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("Dal.File.FileDal", "LogoFile")
                        .WithMany()
                        .HasForeignKey("LogoFileId");

                    b.HasOne("Dal.File.FileDal", "SongFile")
                        .WithMany()
                        .HasForeignKey("SongFileId");

                    b.HasOne("Dal.Song.SongDal", "Song")
                        .WithMany()
                        .HasForeignKey("SongId");

                    b.Navigation("Author");

                    b.Navigation("LogoFile");

                    b.Navigation("Song");

                    b.Navigation("SongFile");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RisingNotesLib.Models.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RisingNotesLib.Models.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RisingNotesLib.Models.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RisingNotesLib.Models.AppIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlaylistDalSongDal", b =>
                {
                    b.HasOne("Dal.Playlist.PlaylistDal", null)
                        .WithMany()
                        .HasForeignKey("PlaylistListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.Song.SongDal", null)
                        .WithMany()
                        .HasForeignKey("SongListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SongDalUserDal", b =>
                {
                    b.HasOne("Dal.Song.SongDal", null)
                        .WithMany()
                        .HasForeignKey("ExcludedSongListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.BaseUser.UserDal", null)
                        .WithMany()
                        .HasForeignKey("ExcludedUserListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SongDalUserDal1", b =>
                {
                    b.HasOne("Dal.BaseUser.UserDal", null)
                        .WithMany()
                        .HasForeignKey("AddedToFavoriteUserListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dal.Song.SongDal", null)
                        .WithMany()
                        .HasForeignKey("FavoriteSongListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dal.Author.AuthorDal", b =>
                {
                    b.Navigation("MusicClipList");

                    b.Navigation("ShortVideoList");

                    b.Navigation("SongList");
                });

            modelBuilder.Entity("Dal.BaseUser.UserDal", b =>
                {
                    b.Navigation("MusicClipCommentList");

                    b.Navigation("PlaylistList");

                    b.Navigation("ShortVideoCommentList");

                    b.Navigation("SongCommentList");
                });

            modelBuilder.Entity("Dal.Song.SongDal", b =>
                {
                    b.Navigation("CommentList");
                });
#pragma warning restore 612, 618
        }
    }
}
