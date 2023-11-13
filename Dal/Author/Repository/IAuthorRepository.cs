using MainLib.Dal.Repository.Base;
using RisingNotesLib.Models;

namespace Dal.Author.Repository;

public interface IAuthorRepository : IRepository<AuthorDal, Guid>
{
    /// <summary>
    /// Получить краткую информацию об авторе
    /// </summary>
    Task<AuthorDal> GetShortInfoAsync(string authorName);
    
    /// <summary>
    /// Получить краткую информацию об авторе
    /// </summary>
    Task<AuthorDal> GetShortInfoAsync(Guid authorId);

    /// <summary>
    /// Получить автора по его id пользователя
    /// </summary>
    Task<AuthorDal> GetByUserIdAsync(Guid userId);

    /// <summary>
    /// Получить информацию об авторе
    /// </summary>
    Task<AuthorDal> GetInfoAsync(string authorName);

    /// <summary>
    /// Получить список авторов по фильтрам
    /// </summary>
    Task<List<AuthorDal>> GetListAsync(GetAuthorListFilterModel filter);
}