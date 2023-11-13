using AutoMapper;

namespace Api.MappingProfile;

public class FeaturedAuthorProfile : Profile
{
    public FeaturedAuthorProfile()
    {
        // // если чел поставил галочку, что нет в сервисе => берем из поля Name
        // CreateMap<AddFeaturedAuthorRequest, FeatAuthorDal>()
        //     .ForMember(d => d.Name,
        //         o =>
        //         {
        //             o.PreCondition(c => c.AuthorId == null);
        //             o.MapFrom(s => s.AuthorName);
        //         })
        //     .ForMember(d => d.AuthorModelId,
        //         o =>
        //         {
        //             o.PreCondition(c => c.AuthorName == null);
        //             o.MapFrom(s => s.AuthorId);
        //         })
        //     .ForMember(d => d.AuthorModel, o => o.Ignore())
        //     .ForMember(d => d.SongList, o => o.Ignore())
        //     .ForMember(d => d.Id, o => o.Ignore());
    }
}