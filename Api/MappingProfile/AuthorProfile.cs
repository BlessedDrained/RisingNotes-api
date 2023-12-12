using Api.Controllers.Author.Dto.Request;
using Api.Controllers.Author.Dto.Response;
using AutoMapper;
using Dal.Author;
using RisingNotesLib.Models;

namespace Api.MappingProfile;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<GetAuthorListRequest, GetAuthorListFilterModel>()
            .ForMember(d => d.NameWildcard, o => o.MapFrom(s => s.NameWildcard));

        CreateMap<MakeAuthorRequest, AuthorDal>()
            .ForMember(d => d.About, o => o.MapFrom(s => s.About))
            .ForMember(d => d.VkLink, o => o.MapFrom(s => s.VkLink))
            .ForMember(d => d.WebSiteLink, o => o.MapFrom(s => s.WebSiteLink))
            .ForMember(d => d.YaMusicLink, o => o.MapFrom(s => s.YaMusicLink))
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.User, o => o.Ignore())
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.SongList, o => o.Ignore())
            .ForMember(d => d.SubscribedUserList, o => o.Ignore());

        CreateMap<AuthorDal, GetAuthorInfoResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.About, o => o.MapFrom(s => s.About))
            .ForMember(d => d.YaMusicLink, o => o.MapFrom(s => s.YaMusicLink))
            .ForMember(d => d.WebSiteLink, o => o.MapFrom(s => s.WebSiteLink))
            .ForMember(d => d.VkLink, o => o.MapFrom(s => s.VkLink));

        CreateMap<UpdateAuthorRequest, AuthorDal>()
            .ForMember(d => d.About, o => o.MapFrom(s => s.About))
            .ForMember(d => d.VkLink, o => o.MapFrom(s => s.VkLink))
            .ForMember(d => d.WebSiteLink, o => o.MapFrom(s => s.WebSiteLink))
            .ForMember(d => d.VkLink, o => o.MapFrom(s => s.VkLink));
    }
}