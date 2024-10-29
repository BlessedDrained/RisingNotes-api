using System.ComponentModel.DataAnnotations;
using Api.Controllers.Profile.Dto.Request;
using Api.Premanager.Auth;
using Dal.Author;
using Dal.Author.Repository;
using Dal.BaseUser.Repository;
using Dal.Context;
using Dal.File.Repository;
using Logic.Author;
using Logic.File;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using MainLib.Dal.Exception;
using MainLib.Enums;
using MainLib.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Exceptions;

namespace Api.Controllers.Debug;

/// <summary>
/// Контроллер для технических целей
/// </summary>
[Route("debug")]
public class DebugController : PublicController
{
    private readonly ApplicationContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IProfilePremanager _profilePremanager;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IAuthorManager _authorManager;

    public DebugController(
        ApplicationContext context,
        RoleManager<IdentityRole> roleManager,
        IProfilePremanager profilePremanager,
        IUserRepository userRepository,
        IAuthorRepository authorRepository,
        IAuthorManager authorManager)
    {
        _context = context;
        _roleManager = roleManager;
        _profilePremanager = profilePremanager;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
        _authorManager = authorManager;
    }


    /// <summary>
    /// Привести БД к изначальному состоянию
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> RestoreDatabase()
    {
        await _context.Database.EnsureCreatedAsync();

        await _roleManager.CreateAsync(new IdentityRole(RoleConstants.Author));
        await _roleManager.CreateAsync(new IdentityRole(RoleConstants.Admin));
        await _roleManager.CreateAsync(new IdentityRole(RoleConstants.User));

        var userRegisterRequest = new RegisterRequest()
        {
            Email = "testuser@mail.ru",
            Password = "123456",
            UserName = "testuser"
        };

        var defaultUserResponse = await _profilePremanager.RegisterAsync(userRegisterRequest);

        var defaultUser = await _userRepository.GetAsync(defaultUserResponse.UserId);
        await _userRepository.UpdateAsync(defaultUser);

        var ziaRegisterRequest = new RegisterRequest()
        {
            Email = "testauthor@mail.ru",
            Password = "123456",
            UserName = "ZIA"
        };

        var ziaRegisterResponse = await _profilePremanager.RegisterAsync(ziaRegisterRequest);

        await _profilePremanager.ChangeRoleAsync(ziaRegisterResponse.IdentityUserGuid.ToString(), RoleConstants.Author);

        var ziaUser = await _userRepository.GetAsync(ziaRegisterResponse.UserId);
        await _userRepository.UpdateAsync(ziaUser);

        var zia = new AuthorDal()
        {
            UserId = ziaUser.Id,
            User = ziaUser,
            About = "Я крутая певица. У меня большое будущее!",
            VkLink = "https://vk.com/lizokshmelik",
            WebSiteLink = "Нету)",
            YaMusicLink = "Нету)",

        };

        await _authorManager.CreateAsync(ziaUser.Id, zia);

        var adminRegisterRequest = new RegisterRequest()
        {
            Email = "testadmin@mail.ru",
            Password = "123456",
            UserName = "Francis Owens"
        };

        var adminRegisterResponse = await _profilePremanager.RegisterAsync(adminRegisterRequest);
        await _profilePremanager.ChangeRoleAsync(adminRegisterResponse.IdentityUserGuid.ToString(), RoleConstants.Admin);

        var francisOwensUser = await _userRepository.GetAsync(adminRegisterResponse.UserId);
        await _userRepository.UpdateAsync(francisOwensUser);

        var francisOwens = new AuthorDal()
        {
            UserId = francisOwensUser.Id,
            User = francisOwensUser,
            About = "Играю на пианино, гитаре. Коллекционирую пластинки.",
            YaMusicLink = "https://music.yandex.ru/artist/11389103",
            WebSiteLink = "В процессе!",
            VkLink = "https://vk.com/id211233345"
        };

        await _authorManager.CreateAsync(francisOwensUser.Id, francisOwens);

        return NoContent();
    }

    /// <summary>
    /// Тестовый рест для проверки ExceptionHandler
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UserRegistrationException"></exception>
    [HttpGet("{custom:bool}")]
    public async Task<IActionResult> TestException([FromRoute, Required] bool? custom)
    {
        if (custom == true)
        {
            throw new EntityNotFoundException<object>(null);
        }

        throw new Exception();
    }

    [HttpPost("log")]
    public async Task<IActionResult> TestSeqLog()
    {
        using var log = new MethodLog();

        return Ok();
    }

    [HttpPost("migrate")]
    public async Task<IActionResult> MigrateFromDbToS3Async(
        [FromServices] IFileRepository fileRepository, 
        [FromServices] IFileManager fileManager)
    {
        var idList = await fileRepository.GetAllDbStoredIdListAsync();
        foreach (var fileId in idList)
        {
            var file = await fileRepository.GetAsync(fileId);
            await fileManager.UploadSingleAsync(file);
            file.StorageType = StorageType.S3Storage;
            await fileRepository.UpdateAsync(file);
        }

        return NoContent();
    }
}