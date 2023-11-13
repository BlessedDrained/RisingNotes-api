using Api.Controllers.Playlist.Dto.Request;
using Api.Controllers.Playlist.Dto.Response;
using AutoMapper;
using Dal.Playlist;

namespace Api.MappingProfile;

/// <inheritdoc />
public class PlaylistProfile : Profile
{
    /// <inheritdoc />
    public PlaylistProfile()
    {
        CreateMap<CreatePlaylistRequest, PlaylistDal>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CreatorId, o => o.Ignore())
            .ForMember(d => d.Creator, o => o.Ignore())
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.IsPrivate, o => o.Ignore())
            .ForMember(d => d.SongList, o => o.Ignore())
            .ForMember(d => d.Creator, o => o.Ignore())
            .ForMember(d => d.CreatorId, o => o.Ignore())
            .ForMember(d => d.LogoFile, o => o.MapFrom(s => s.LogoFile))
            .ForMember(d => d.LogoFileId, o => o.Ignore());

        CreateMap<PlaylistDal, GetPlaylistInfoResponse>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));
    }
}