using MainLib.CustomException;

namespace MainLib.Dal.Exception;

/// <summary>
/// Сущность не найдена в репозитории
/// </summary>
public class EntityNotFoundException<TDal> : BadRequestException
{
    public EntityNotFoundException(object id) : base($"Entity of type {typeof(TDal).Name} not found by Id = {id}")
    {
    }
}