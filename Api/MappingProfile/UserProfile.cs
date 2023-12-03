using Api.Controllers.Profile.Dto.Response;
using AutoMapper;
using RisingNotesLib.Models;

namespace Api.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // CreateMap<UserDal, AuthorDal>()
        //     .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
        //     .ForMember(d => d.About, o => o.Ignore())
        //     .ForMember(d => d.Name, o => o.Ignore())
        //     .ForMember(d => d.SongList, o => o.Ignore())
        //     .ForMember(d => d.YaMusicLink, o => o.Ignore())
        //     .ForMember(d => d.VkLink, o => o.Ignore())
        //     .ForMember(d => d.WebSiteLink, o => o.Ignore())
        //     .ForMember(d => d.CommentList, o => o.MapFrom(s => s.CommentList))
        //     .ForMember(d => d.LogoFile, o => o.MapFrom(s => s.LogoFile))
        //     .ForMember(d => d.IdentityUser, o => o.MapFrom(s => s.IdentityUser))
        //     .ForMember(d => d.FavoriteSongList, o => o.MapFrom(s => s.FavoriteSongList))
        //     .ForMember(d => d.LogoFileId, o => o.MapFrom(s => s.LogoFileId))
        //     .ForMember(d => d.IdentityUserId, o => o.MapFrom(s => s.IdentityUserId))
        //     .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
        //     .ReverseMap();

        CreateMap<AppIdentityUser, GetProfileResponse>()
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));
    }
}