namespace MainLib.Enums;

/// <summary>
/// Тип пользователя
/// </summary>
public enum UserType
{
    /// <summary>
    /// Значение по умолчанию
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Пользователь
    /// </summary>
    User = 1,
    
    /// <summary>
    /// Автор
    /// </summary>
    Author = 2,
    
    /// <summary>
    /// Администратор
    /// </summary>
    Admin = 3
}