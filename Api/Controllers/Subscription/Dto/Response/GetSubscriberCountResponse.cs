namespace Api.Controllers.Subscription.Dto.Response;

/// <summary>
/// Ответ на получение списка подписчиков автора
/// </summary>
public record GetSubscriberCountResponse
{
    /// <summary>
    /// Список подписчиков
    /// </summary>
    public int Count { get; init; }
}