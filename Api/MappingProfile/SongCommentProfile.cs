using Api.Controllers.SongComment.Response;
using AutoMapper;
using Dal.SongComment;

namespace Api.MappingProfile;

public class SongCommentProfile : Profile
{
    public SongCommentProfile()
    {
        CreateMap<SongCommentDal, GetSongCommentResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AuthorId, o => o.MapFrom(s => s.CreatorId))
            .ForMember(d => d.Text, o => o.MapFrom(s => s.Text))
            .ForMember(d => d.AuthorDisplayedName, o => o.Ignore())
            .ForMember(d => d.IsSongAuthor, o => o.Ignore());
    }
}