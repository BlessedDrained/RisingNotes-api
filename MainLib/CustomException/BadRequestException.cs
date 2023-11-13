namespace MainLib.CustomException;

/// <summary>
/// Исключение с кодом ошибки
/// </summary>
public abstract class BadRequestException : Exception
{
    ///
    protected BadRequestException(string message) : base(message)
    {

    }
}