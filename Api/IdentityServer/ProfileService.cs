using System.Security.Claims;
using Dal.Author.Repository;
using Dal.BaseUser.Repository;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using RisingNotesLib.Constant;
using RisingNotesLib.Models;

namespace Api.IdentityServer;

/// <inheritdoc />
public class ProfileService : IProfileService
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;

    public ProfileService(UserManager<AppIdentityUser> userManager, IUserRepository userRepository, IAuthorRepository authorRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
    }

    /// <inheritdoc />
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);

        var claimList = await GetUserClaimListAsync(user);
        context.IssuedClaims.AddRange(claimList);

        var userId = await _userRepository.GetUserIdByIdentityUserGuid(Guid.Parse(sub));
        var subClaim = new Claim(ClaimTypes.Name, userId.ToString());
        var exists = await _authorRepository.ExistsAsync(x => x.UserId == userId);
        if (exists)
        {
            var author = await _authorRepository.GetByUserIdAsync(userId);
            context.IssuedClaims.Add(new Claim(ClaimTypeConstants.AuthorId, author.Id.ToString()));
        }

        context.IssuedClaims.Add(subClaim);
    }

    /// <inheritdoc />
    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user != null;
    }

    /// <summary>
    /// Получить клеймы для ролей
    /// </summary>
    private async Task<List<Claim>> GetUserClaimListAsync(AppIdentityUser appIdentityUser)
    {
        var roleList = await _userManager.GetRolesAsync(appIdentityUser);

        return roleList
            .Select(role => new Claim(JwtClaimTypes.Role, role))
            .ToList();
    }
}