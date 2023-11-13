﻿using Dal.Author;
using Dal.Author.Repository;
using Dal.BaseUser.Repository;
using Dal.Song;
using Dal.Song.Repository;
using MainLib.Api.Auth.Constant;
using MainLib.Logging;
using Microsoft.AspNetCore.Identity;
using RisingNotesLib.Models;

namespace Logic.Author;

/// <inheritdoc />
public class AuthorManager : IAuthorManager
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ISongRepository _songRepository;

    public AuthorManager(
        UserManager<AppIdentityUser> userManager,
        IUserRepository userRepository,
        IAuthorRepository authorRepository,
        ISongRepository songRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
        _songRepository = songRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Guid userId, AuthorDal author)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user.IsAuthor)
        {
            // бросать исключение, что пользователь уже является автором
        }

        author.User = user;
        author.UserId = user.Id;

        await _authorRepository.InsertAsync(author);

        var identityUserId = await _userRepository.GetIdentityUserGuid(userId);
        var identityUser = await _userManager.FindByIdAsync(identityUserId.ToString());
        var roleList = await _userManager.GetRolesAsync(identityUser);

        if (roleList.Contains(RoleConstants.User))
        {
            await _userManager.RemoveFromRolesAsync(identityUser, roleList);
            await _userManager.AddToRoleAsync(identityUser, RoleConstants.Author);
            user.IsAuthor = true;
        }
        
        return author.Id;
    }

    /// <inheritdoc />
    public async Task<List<SongDal>> GetAuthorSongInfoListAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var songInfoList = await _songRepository.GetListAsync(x => x.AuthorId == authorId);

        log.ReturnsValue(songInfoList);
        return songInfoList;
    }
}