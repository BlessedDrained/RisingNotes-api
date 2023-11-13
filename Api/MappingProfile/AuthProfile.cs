using Api.Controllers.Profile.Dto.Request;
using AutoMapper;
using RisingNotesLib.Models;

namespace Api.MappingProfile;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequest, AppIdentityUser>()
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.LockoutEnabled, o => o.Ignore())
            .ForMember(d => d.ConcurrencyStamp, o => o.Ignore())
            .ForMember(d => d.EmailConfirmed, o => o.Ignore())
            .ForMember(d => d.NormalizedEmail, o => o.Ignore())
            .ForMember(d => d.PasswordHash, o => o.Ignore())
            .ForMember(d => d.PhoneNumber, o => o.Ignore())
            .ForMember(d => d.SecurityStamp, o => o.Ignore())
            // TODO: 
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email))
            .ForMember(d => d.AccessFailedCount, o => o.Ignore())
            .ForMember(d => d.PhoneNumberConfirmed, o => o.Ignore())
            .ForMember(d => d.TwoFactorEnabled, o => o.Ignore())
            .ForMember(d => d.NormalizedUserName, o => o.Ignore())
            .ForMember(d => d.LockoutEnd, o => o.Ignore());

    }
}