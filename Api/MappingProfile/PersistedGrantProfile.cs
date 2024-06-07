using AutoMapper;
using Dal.PersistedGrant;
using IdentityServer4.Models;

namespace Api.MappingProfile;

/// <inheritdoc />
public class PersistedGrantProfile : Profile
{
    public PersistedGrantProfile()
    {
        CreateMap<PersistedGrant, PersistedGrantDal>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s.Data))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.ClientId, o => o.MapFrom(s => s.ClientId))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ForMember(d => d.Expiration, o => o.MapFrom(s => s.Expiration))
            .ForMember(d => d.Key, o => o.MapFrom(s => s.Key))
            .ForMember(d => d.ConsumedTime, o => o.MapFrom(s => s.ConsumedTime))
            .ForMember(d => d.CreationTime, o => o.MapFrom(s => s.CreationTime))
            .ForMember(d => d.SessionId, o => o.MapFrom(s => s.SessionId))
            .ForMember(d => d.SubjectId, o => o.MapFrom(s => s.SubjectId))
            .ForMember(d => d.Id, o => o.Ignore())
            .ReverseMap();
    }
}