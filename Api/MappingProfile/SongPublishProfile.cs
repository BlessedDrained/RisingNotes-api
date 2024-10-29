using Api.Controllers.SongPublish.Dto.Request;
using Api.Controllers.SongPublish.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.Song;
using Dal.SongPublish;
using RisingNotesLib.Models;

namespace Api.MappingProfile;

public class SongPublishProfile : Profile
{
    public SongPublishProfile()
    {
        CreateMap<CreateSongPublishRequestRequest, SongPublishRequestDal>()
            .ForMember(d => d.SongFile, o => o.Ignore())
            .ForMember(d => d.LogoFile, o => o.Ignore())
            .ForMember(d => d.Status, o => o.Ignore())
            .ForMember(d => d.Song, o => o.Ignore())
            .ForMember(d => d.SongId, o => o.Ignore())
            .ForMember(d => d.Lyrics, o => o.MapFrom(s => s.Lyrics))
            .ForMember(d => d.ReviewerComment, o => o.Ignore())
            .ForMember(d => d.AuthorId, o => o.Ignore())
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.SongFileId, o => o.Ignore())
            .ForMember(d => d.LogoFileId, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Author, o => o.Ignore())
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList))
            .ForMember(d => d.VibeList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.LanguageList, o => o.MapFrom(s => s.LanguageList))
            .ForMember(d => d.Instrumental, o => o.MapFrom(s => s.Instrumental))
            .ForMember(d => d.VocalGenderList, o => o.MapFrom(s => s.VocalGenderList))
            .ForMember(d => d.DurationMs, o => o.Ignore());

        CreateMap<GetPublishRequestListRequest, GetPublishRequestListFilterModel>()
            .ForMember(d => d.OrderByStatusDescending, o => o.MapFrom(s => s.OrderByStatusDescending))
            .ForMember(d => d.OrderByAuthorNameDescending, o => o.MapFrom(s => s.OrderByAuthorNameDescending))
            .ForMember(d => d.Count, o => o.MapFrom(s => s.Count))
            .ForMember(d => d.Offset, o => o.MapFrom(s => s.Offset));

        CreateMap<SongPublishRequestDal, SongDal>()
            .ForMember(d => d.SongFileId, o => o.MapFrom(s => s.SongFileId))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
            .ForMember(d => d.Lyrics, o => o.MapFrom(s => s.Lyrics))
            .ForMember(d => d.LogoFile, o => o.MapFrom(s => s.LogoFile))
            .ForMember(d => d.Author, o => o.MapFrom(s => s.Author))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.CommentList, o => o.Ignore())
            .ForMember(d => d.DurationMsec, o => o.Ignore())
            .ForMember(d => d.PlaylistList, o => o.Ignore())
            .ForMember(d => d.SongFile, o => o.MapFrom(s => s.SongFile))
            .ForMember(d => d.LogoFileId, o => o.MapFrom(s => s.LogoFileId))
            .ForMember(d => d.AddedToFavoriteUserList, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.VibeList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.LanguageList, o => o.MapFrom(s => s.LanguageList))
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList))
            .ForMember(d => d.Instrumental, o => o.MapFrom(s => s.Instrumental))
            .ForMember(d => d.AuditionCount, o => o.Ignore())
            .ForMember(d => d.ExcludedUserList, o => o.Ignore());

        CreateMap<ReplyToRequestAsUserRequest, SongPublishRequestDal>()
            .ForMember(d => d.SongFile, o => o.MapFrom(s => s.SongFile))
            .ForMember(d => d.LogoFile, o => o.MapFrom(s => s.LogoFile))
            .ForMember(d => d.Status, o => o.Ignore())
            .ForMember(d => d.Song, o => o.Ignore())
            .ForMember(d => d.SongId, o => o.Ignore())
            .ForMember(d => d.Lyrics, o => o.MapFrom(s => s.Lyrics))
            .ForMember(d => d.ReviewerComment, o => o.Ignore())
            .ForMember(d => d.AuthorId, o => o.Ignore())
            .ForMember(d => d.Name, o => o.MapFrom(s => s.SongName))
            .ForMember(d => d.SongFileId, o => o.Ignore())
            .ForMember(d => d.LogoFileId, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Author, o => o.Ignore())
            .ForMember(d => d.VibeList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList))
            .ForMember(d => d.LanguageList, o => o.MapFrom(s => s.LanguageList))
            .ForMember(d => d.Instrumental, o => o.MapFrom(s => s.Instrumental))
            .ForMember(d => d.DurationMs, o => o.Ignore());

        CreateMap<SongPublishRequestDal, GetPublishRequestShortInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
            .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.User.UserName))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId));

        CreateMap<SongPublishRequestDal, GetPublishRequestInfoResponse>()
            .ForMember(d => d.Lyrics, o => o.MapFrom(s => s.Lyrics))
            .ForMember(d => d.SongName, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.LogoFile, o => o.MapFrom(s => s.LogoFile.ToFileContent()))
            .ForMember(d => d.SongFile, o => o.MapFrom(s => s.SongFile.ToFileContent()))
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList))
            .ForMember(d => d.VibeList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.LanguageList, o => o.MapFrom(s => s.LanguageList))
            .ForMember(d => d.Instrumental, o => o.MapFrom(s => s.Instrumental))
            .ForMember(d => d.PublishedSongId, o => o.MapFrom(s => s.SongId))
            .ForMember(d => d.DurationMs, o => o.MapFrom(s => s.DurationMs))
            .ForMember(d => d.ReviewerComment, o => o.MapFrom(s => s.ReviewerComment));
    }
}