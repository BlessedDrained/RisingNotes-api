﻿using Api.Controllers.Song.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using AutoMapper;
using Dal.Song;

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
            .ForMember(d => d.LogoFileId, o => o.Ignore());

        CreateMap<SongDal, GetSongInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.DurationMs, o => o.MapFrom(s => s.DurationMsec))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId));

        CreateMap<SongDal, GetWithAuthorSongInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.AuthorId))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.DurationMs, o => o.MapFrom(s => s.DurationMsec))
            .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.Name));
    }
}