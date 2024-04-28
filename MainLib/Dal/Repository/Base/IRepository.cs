using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace MainLib.Dal.Repository.Base;

/// <summary>
/// Интерфейс для репозитория
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип первичного ключа</typeparam>
public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Создание сущности
    /// </summary>
    /// <param name="entity">сущность для добавления</param>
    /// <returns>Идентификатор созданной сущности</returns>
    Task<TKey> InsertAsync(TEntity entity);
    
    /// <summary>
    /// Получение сущности
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Найденная сущность</returns>
    Task<TEntity> GetAsync(TKey id);
    
    // /// <summary>
    // /// Получение списка сущностей по списку их идентификаторов
    // /// </summary>
    // /// <param name="idList">Список идентификаторов</param>
    // /// <returns>Список сущностей</returns>
    // Task<IEnumerable<TEntity>> GetByIdListAsync(IEnumerable<TKey> idList);
    
    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity">Сущность, которую нужно обновить</param>
    Task UpdateAsync(TEntity entity);
    
    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="id">Идентификатор, сущности, которую нужно удалить</param>
    Task DeleteAsync(TKey id);
    
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    /// <returns>Список всех сущностей</returns>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null);
    
    // /// <summary>
    // /// Получение списка сущностей с учетом фильтрации и сортировки
    // /// </summary>
    // /// <param name="filter">Expression с фильтрами</param>
    // /// <param name="orderBy">Expression для сортировки</param>
    // /// <param name="descending">Сортировать по убыванию</param>
    // /// <returns>Список сущностей</returns>
    // Task<IEnumerable<TEntity>> GetListAsync(
    //     Expression<Func<TEntity, bool>>? filter = null,
    //     Expression<Func<TEntity, object>>? orderBy = null,
    //     bool descending = false);

    /// <summary>
    /// Получение количества записей с учетом фильтра
    /// </summary>
    /// <param name="filter">Expression с фильтрами</param>
    /// <returns>Количество записей</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);

    /// <summary>
    /// Проверить, существует ли сущность с заданными параметрами
    /// </summary>
    /// <param name="filter">Expression с фильтрами</param>
    /// <returns>Флаг, существует ли сущность</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter);

    /// <summary>
    /// Начать транзакцию
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// Начать транзакцию или получить уже существующую
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionOrExistingAsync();
}