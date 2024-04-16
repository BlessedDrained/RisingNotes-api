namespace MainLib.CustomException;

/// <summary>
/// Исключение с кодом ошибки
/// </summary>
public abstract class BadRequestException : Exception
{
    public int ErrorCode { get; init; }
    
    ///
    protected BadRequestException(string message) : base(message)
    {
        
    }

    protected BadRequestException(string message, int errorCode) 
        : base(message)
    {
        ErrorCode = errorCode;
    }
}