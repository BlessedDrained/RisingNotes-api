using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Extension = table.Column<string>(type: "text", nullable: true),
                    StorageType = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrantDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    SubjectId = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    ClientId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrantDal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    LogoFileId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsAuthor = table.Column<bool>(type: "boolean", nullable: false),
                    IdentityUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDal_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserDal_FileDal_LogoFileId",
                        column: x => x.LogoFileId,
                        principalTable: "FileDal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthorDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    About = table.Column<string>(type: "text", nullable: true),
                    VkLink = table.Column<string>(type: "text", nullable: true),
                    YaMusicLink = table.Column<string>(type: "text", nullable: true),
                    WebSiteLink = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorDal_UserDal_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClipCommentDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SongId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClipCommentDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClipCommentDal_UserDal_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false),
                    LogoFileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistDal_FileDal_LogoFileId",
                        column: x => x.LogoFileId,
                        principalTable: "FileDal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlaylistDal_UserDal_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortVideoCommentDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    ShortVideoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortVideoCommentDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortVideoCommentDal_UserDal_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorDalUserDal",
                columns: table => new
                {
                    SubscribedUserListId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorDalUserDal", x => new { x.SubscribedUserListId, x.SubscriptionListId });
                    table.ForeignKey(
                        name: "FK_AuthorDalUserDal_AuthorDal_SubscriptionListId",
                        column: x => x.SubscriptionListId,
                        principalTable: "AuthorDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorDalUserDal_UserDal_SubscribedUserListId",
                        column: x => x.SubscribedUserListId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClipDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PreviewFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClipFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    SongId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DurationMsec = table.Column<int>(type: "integer", nullable: false),
                    UploaderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClipDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClipDal_AuthorDal_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AuthorDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortVideoDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PreviewFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    VideoFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelatedSongId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DurationMsec = table.Column<int>(type: "integer", nullable: false),
                    UploaderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortVideoDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShortVideoDal_AuthorDal_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "AuthorDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenreList = table.Column<string[]>(type: "text[]", nullable: true),
                    VibeList = table.Column<string[]>(type: "text[]", nullable: true),
                    LanguageList = table.Column<string[]>(type: "text[]", nullable: true),
                    VocalGenderList = table.Column<int[]>(type: "integer[]", nullable: true),
                    Instrumental = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DurationMsec = table.Column<int>(type: "integer", nullable: false),
                    Lyrics = table.Column<string>(type: "text", nullable: true),
                    SongFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogoFileId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditionCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongDal_AuthorDal_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AuthorDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongDal_FileDal_LogoFileId",
                        column: x => x.LogoFileId,
                        principalTable: "FileDal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SongDal_FileDal_SongFileId",
                        column: x => x.SongFileId,
                        principalTable: "FileDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistDalSongDal",
                columns: table => new
                {
                    PlaylistListId = table.Column<Guid>(type: "uuid", nullable: false),
                    SongListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistDalSongDal", x => new { x.PlaylistListId, x.SongListId });
                    table.ForeignKey(
                        name: "FK_PlaylistDalSongDal_PlaylistDal_PlaylistListId",
                        column: x => x.PlaylistListId,
                        principalTable: "PlaylistDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistDalSongDal_SongDal_SongListId",
                        column: x => x.SongListId,
                        principalTable: "SongDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongCommentDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SongId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongCommentDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongCommentDal_SongDal_SongId",
                        column: x => x.SongId,
                        principalTable: "SongDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongCommentDal_UserDal_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongDalUserDal",
                columns: table => new
                {
                    ExcludedSongListId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExcludedUserListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDalUserDal", x => new { x.ExcludedSongListId, x.ExcludedUserListId });
                    table.ForeignKey(
                        name: "FK_SongDalUserDal_SongDal_ExcludedSongListId",
                        column: x => x.ExcludedSongListId,
                        principalTable: "SongDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongDalUserDal_UserDal_ExcludedUserListId",
                        column: x => x.ExcludedUserListId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongDalUserDal1",
                columns: table => new
                {
                    AddedToFavoriteUserListId = table.Column<Guid>(type: "uuid", nullable: false),
                    FavoriteSongListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongDalUserDal1", x => new { x.AddedToFavoriteUserListId, x.FavoriteSongListId });
                    table.ForeignKey(
                        name: "FK_SongDalUserDal1_SongDal_FavoriteSongListId",
                        column: x => x.FavoriteSongListId,
                        principalTable: "SongDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongDalUserDal1_UserDal_AddedToFavoriteUserListId",
                        column: x => x.AddedToFavoriteUserListId,
                        principalTable: "UserDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongPublishRequestDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ReviewerComment = table.Column<string>(type: "text", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: true),
                    SongId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Lyrics = table.Column<string>(type: "text", nullable: true),
                    Instrumental = table.Column<bool>(type: "boolean", nullable: false),
                    DurationMs = table.Column<int>(type: "integer", nullable: false),
                    GenreList = table.Column<string[]>(type: "text[]", nullable: true),
                    VibeList = table.Column<string[]>(type: "text[]", nullable: true),
                    LanguageList = table.Column<string[]>(type: "text[]", nullable: true),
                    VocalGenderList = table.Column<int[]>(type: "integer[]", nullable: true),
                    SongFileId = table.Column<Guid>(type: "uuid", nullable: true),
                    LogoFileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongPublishRequestDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SongPublishRequestDal_AuthorDal_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AuthorDal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SongPublishRequestDal_FileDal_LogoFileId",
                        column: x => x.LogoFileId,
                        principalTable: "FileDal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SongPublishRequestDal_FileDal_SongFileId",
                        column: x => x.SongFileId,
                        principalTable: "FileDal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SongPublishRequestDal_SongDal_SongId",
                        column: x => x.SongId,
                        principalTable: "SongDal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorDal_UserId",
                table: "AuthorDal",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorDalUserDal_SubscriptionListId",
                table: "AuthorDalUserDal",
                column: "SubscriptionListId");

            migrationBuilder.CreateIndex(
                name: "IX_ClipCommentDal_CreatorId",
                table: "ClipCommentDal",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ClipDal_UploaderId",
                table: "ClipDal",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistDal_CreatorId",
                table: "PlaylistDal",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistDal_LogoFileId",
                table: "PlaylistDal",
                column: "LogoFileId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistDalSongDal_SongListId",
                table: "PlaylistDalSongDal",
                column: "SongListId");

            migrationBuilder.CreateIndex(
                name: "IX_ShortVideoCommentDal_CreatorId",
                table: "ShortVideoCommentDal",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShortVideoDal_UploaderId",
                table: "ShortVideoDal",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_SongCommentDal_CreatorId",
                table: "SongCommentDal",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SongCommentDal_SongId",
                table: "SongCommentDal",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDal_AuthorId",
                table: "SongDal",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDal_LogoFileId",
                table: "SongDal",
                column: "LogoFileId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDal_SongFileId",
                table: "SongDal",
                column: "SongFileId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDalUserDal_ExcludedUserListId",
                table: "SongDalUserDal",
                column: "ExcludedUserListId");

            migrationBuilder.CreateIndex(
                name: "IX_SongDalUserDal1_FavoriteSongListId",
                table: "SongDalUserDal1",
                column: "FavoriteSongListId");

            migrationBuilder.CreateIndex(
                name: "IX_SongPublishRequestDal_AuthorId",
                table: "SongPublishRequestDal",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_SongPublishRequestDal_LogoFileId",
                table: "SongPublishRequestDal",
                column: "LogoFileId");

            migrationBuilder.CreateIndex(
                name: "IX_SongPublishRequestDal_SongFileId",
                table: "SongPublishRequestDal",
                column: "SongFileId");

            migrationBuilder.CreateIndex(
                name: "IX_SongPublishRequestDal_SongId",
                table: "SongPublishRequestDal",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDal_IdentityUserId",
                table: "UserDal",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDal_LogoFileId",
                table: "UserDal",
                column: "LogoFileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorDalUserDal");

            migrationBuilder.DropTable(
                name: "ClipCommentDal");

            migrationBuilder.DropTable(
                name: "ClipDal");

            migrationBuilder.DropTable(
                name: "PersistedGrantDal");

            migrationBuilder.DropTable(
                name: "PlaylistDalSongDal");

            migrationBuilder.DropTable(
                name: "ShortVideoCommentDal");

            migrationBuilder.DropTable(
                name: "ShortVideoDal");

            migrationBuilder.DropTable(
                name: "SongCommentDal");

            migrationBuilder.DropTable(
                name: "SongDalUserDal");

            migrationBuilder.DropTable(
                name: "SongDalUserDal1");

            migrationBuilder.DropTable(
                name: "SongPublishRequestDal");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PlaylistDal");

            migrationBuilder.DropTable(
                name: "SongDal");

            migrationBuilder.DropTable(
                name: "AuthorDal");

            migrationBuilder.DropTable(
                name: "UserDal");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FileDal");
        }
    }
}
