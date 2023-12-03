using Api.Controllers.Profile.Dto.Request;
using Api.Controllers.Profile.Dto.Response;
using AutoMapper;
using Dal.BaseUser;
using Dal.BaseUser.Repository;
using Logic.User;
using MainLib.Api.Auth.Constant;
using MainLib.Logging;
using Microsoft.AspNetCore.Identity;
using RisingNotesLib.Exceptions;
using RisingNotesLib.Models;

namespace Api.Premanager.Auth;

/// <inheritdoc />
public class ProfilePremanager : IProfilePremanager
{
    private readonly IMapper _mapper;
    private readonly UserManager<AppIdentityUser> _identityUserManager;
    private readonly IUserManager _userManager;
    private readonly IUserRepository _userRepository;

    public ProfilePremanager(
        IMapper mapper,
        UserManager<AppIdentityUser> identityUserManager,
        IUserManager userManager,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _identityUserManager = identityUserManager;
        _userManager = userManager;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        using var log = new MethodLog(request);
        // TODO: раскомментить, как будет возможность реализовать на фрнте
        // await _captchaValidator.ValidateAsync(registerRequest.CaptchaToken);

        var appUser = _mapper.Map<AppIdentityUser>(request);

        var creationResult = await _identityUserManager.CreateAsync(appUser, request.Password);
        if (!creationResult.Succeeded)
        {
            throw new UserRegistrationException(creationResult.Errors);
        }

        var addToRoleResult = await _identityUserManager.AddToRoleAsync(appUser, RoleConstants.User);
        if (!addToRoleResult.Succeeded)
        {
            await _identityUserManager.DeleteAsync(appUser);
            throw new AddToRoleException(addToRoleResult.Errors);
        }

        var user = new UserDal()
        {
            IdentityUser = appUser,
            IdentityUserId = appUser.Id,
            UserName = request.UserName
        };

        var id = await _userManager.CreateAsync(user);

        var response = new RegisterResponse()
        {
            UserId = id,
            IdentityUserGuid = Guid.Parse(appUser.Id)
        };

        log.ReturnsValue(response);

        return response;
    }

    /// <inheritdoc />
    public async Task ChangeRoleAsync(string userId, string newRoleName)
    {
        using var log = new MethodLog(userId, newRoleName);

        var user = await _identityUserManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception();
            // TODO: исключение, что пользователь не найден
        }

        var alreadyHasRole = await _identityUserManager.IsInRoleAsync(user, newRoleName);
        if (alreadyHasRole)
        {
            throw new Exception();
            // TODO: исключение, что пользователь уже находится в этой роли - нет необходимости ее менять
        }

        var roleList = await _identityUserManager.GetRolesAsync(user);
        await _identityUserManager.RemoveFromRolesAsync(user, roleList);
        await _identityUserManager.AddToRoleAsync(user, newRoleName);

        var id = await _userRepository.GetUserIdByIdentityUserGuid(Guid.Parse(userId));
        var userModel = await _userRepository.GetAsync(id);

        userModel.IsAuthor = newRoleName != RoleConstants.User;

        await _userRepository.UpdateAsync(userModel);
    }

    /// <inheritdoc />
    public async Task<GetProfileResponse> GetProfileAsync(Guid identityUserId)
    {
        var user = await _identityUserManager.FindByIdAsync(identityUserId.ToString());
        if (user == null)
        {
            throw new Exception("Profile not found");
        }

        var profile = _mapper.Map<GetProfileResponse>(user);
        
        return profile;
    }
}