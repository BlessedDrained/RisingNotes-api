using MainLib.CustomException;

namespace MainLib.Dal.Exception;

public class EntityNotFoundException : BadRequestException
{
    public EntityNotFoundException(string message) : base(message)
    {
    }
}

/// <summary>
/// Сущность не найдена в репозитории
/// </summary>
public class EntityNotFoundException<TDal> : EntityNotFoundException
{
    public EntityNotFoundException(object id) : base($"Entity of type {typeof(TDal).Name} not found by Id = {id}")
    {
    }
}