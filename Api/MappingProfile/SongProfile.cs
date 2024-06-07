using Api.Controllers.ExcludedTrack.Dto;
using Api.Controllers.Song.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using AutoMapper;
using Dal.Song;
using RisingNotesLib.Models;

namespace Api.MappingProfile;

/// <inheritdoc />
public class SongProfile : Profile
{
    public SongProfile()
    {
        CreateMap<UploadSongRequest, SongDal>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.SongName))
            .ForMember(d => d.SongFile, o => o.MapFrom(s => s.SongFile))
            .ForMember(d => d.LogoFile, o => o.MapFrom(s => s.SongLogo))
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.AuthorId, o => o.Ignore())
            .ForMember(d => d.Author, o => o.Ignore())
            .ForMember(d => d.DurationMsec, o => o.Ignore())
            .ForMember(d => d.AddedToFavoriteUserList, o => o.Ignore())
            .ForMember(d => d.CommentList, o => o.Ignore())
            .ForMember(d => d.SongFileId, o => o.Ignore())
            .ForMember(d => d.PlaylistList, o => o.Ignore())
            .ForMember(d => d.LogoFileId, o => o.Ignore())
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList))
            .ForMember(d => d.LanguageList, o => o.MapFrom(s => s.LanguageList))
            .ForMember(d => d.VibeList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.Instrumental, o => o.MapFrom(s => s.Instrumental))
            .ForMember(d => d.AuditionCount, o => o.Ignore())
            .ForMember(d => d.VocalGenderList, o => o.MapFrom(s => s.VocalGenderList))
            .ForMember(d => d.ExcludedUserList, o => o.Ignore());

        CreateMap<SongDal, GetAuthorSongInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.DurationMs, o => o.MapFrom(s => s.DurationMsec))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
            .ForMember(d => d.AuditionCount, o => o.MapFrom(s => s.AuditionCount))
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList))
            .ForMember(d => d.VibeList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.LanguageList, o => o.MapFrom(s => s.LanguageList))
            .ForMember(d => d.VocalGenderList, o => o.MapFrom(s => s.VocalGenderList));

        CreateMap<SongDal, GetWithAuthorSongInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Lyrics, o => o.MapFrom(s => s.Lyrics))
            .ForMember(d => d.DurationMs, o => o.MapFrom(s => s.DurationMsec))
            .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.User.UserName))
            .ForMember(d => d.VocalGenderList, o => o.MapFrom(s => s.VocalGenderList));

        CreateMap<GetSongListRequest, GetSongListFilterModel>()
            .ForMember(d => d.Gender, o => o.MapFrom(s => s.Gender))
            .ForMember(d => d.Instrumental, o => o.MapFrom(s => s.Instrumental))
            .ForMember(d => d.LanguageList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.VibeList, o => o.MapFrom(s => s.VibeList))
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList))
            .ForMember(d => d.TrackDurationRange, o => o.MapFrom(s => s.TrackDurationRange));

        CreateMap<SongDal, GetExcludedTrackInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
            .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.User.UserName))
            .ForMember(d => d.DurationMs, o => o.MapFrom(s => s.DurationMsec))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.GenreList, o => o.MapFrom(s => s.GenreList));
    }
}