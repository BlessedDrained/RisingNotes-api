using Api.Controllers.ClipComment.Dto.Response;
using AutoMapper;
using Dal.MusicClipComment;

namespace Api.MappingProfile;

public class MusicClipCommentProfile : Profile
{
    public MusicClipCommentProfile()
    {
        CreateMap<ClipCommentDal, GetClipCommentResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.CreatorId))
            .ForMember(d => d.Text, o => o.MapFrom(s => s.Text))
            .ForMember(d => d.AuthorDisplayedName, o => o.Ignore())
            .ForMember(d => d.IsSongAuthor, o => o.Ignore());
    }
}