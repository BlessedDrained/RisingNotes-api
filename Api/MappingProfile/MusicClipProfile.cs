using Api.Controllers.MusicClip.Dto.Request;
using Api.Controllers.MusicClip.Dto.Response;
using AutoMapper;
using Dal.MusicClip;

namespace Api.MappingProfile;

public class MusicClipProfile : Profile
{
    public MusicClipProfile()
    {
        CreateMap<MusicClipDal, GetMusicClipInfoResponse>()
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.SongId, o => o.MapFrom(s => s.SongId))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
            .ForMember(d => d.UploaderId, o => o.MapFrom(s => s.UploaderId))
            .ForMember(d => d.ClipFileId, o => o.MapFrom(s => s.ClipFileId))
            .ForMember(d => d.PreviewFileId, o => o.MapFrom(s => s.PreviewFileId));

        CreateMap<UploadClipRequest, MusicClipDal>()
            .ForMember(d => d.SongId, o => o.MapFrom(s => s.SongId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
            .ForMember(d => d.PreviewFileId, o => o.Ignore())
            .ForMember(d => d.ClipFileId, o => o.Ignore())
            .ForMember(d => d.Uploader, o => o.Ignore())
            .ForMember(d => d.UploaderId, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.DurationMsec, o => o.Ignore());
    }
}