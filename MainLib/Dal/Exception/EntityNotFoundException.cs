using MainLib.CustomException;
using MainLib.Exceptions;

namespace MainLib.Dal.Exception;

public class EntityNotFoundException : BadRequestException
{
    public EntityNotFoundException(string message) : base(message, MainErrorConstants.EntityNotFound)
    {
    }
}

/// <summary>
/// Сущность не найдена в репозитории
/// </summary>
public class EntityNotFoundException<TDal> : EntityNotFoundException
{
    public EntityNotFoundException(object id) : base($"Entity of type {typeof(TDal).Name} was not found by Id = {id}")
    {
    }

    public EntityNotFoundException() : base($"Entity of type {typeof(TDal)} was not found")
    {
        
    } 
}