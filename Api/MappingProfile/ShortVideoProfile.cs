using Api.Controllers.ShortVideo.Dto.Request;
using Api.Controllers.ShortVideo.Dto.Response;
using AutoMapper;
using Dal.ShortVideo;

namespace Api.MappingProfile;

public class ShortVideoProfile : Profile
{
    public ShortVideoProfile()
    {
        CreateMap<UploadShortVideoRequest, ShortVideoDal>()
            .ForMember(x => x.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
            .ForMember(d => d.Uploader, o => o.Ignore())
            .ForMember(d => d.UploaderId, o => o.Ignore())
            .ForMember(d => d.DurationMsec, o => o.Ignore())
            .ForMember(d => d.PreviewFileId, o => o.Ignore())
            .ForMember(d => d.RelatedSongId, o => o.Ignore())
            .ForMember(x => x.VideoFileId, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore());

        CreateMap<ShortVideoDal, GetShortVideoInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.PreviewFileId, o => o.MapFrom(s => s.PreviewFileId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.UploaderId, o => o.MapFrom(s => s.UploaderId))
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
            .ForMember(d => d.VideoFileId, o => o.MapFrom(s => s.VideoFileId))
            .ForMember(d => d.RelatedSongId, o => o.MapFrom(s => s.RelatedSongId));
    }
}