namespace Api.Controllers.Subscription.Dto.Response;

/// <summary>
/// Ответ на получение информации о подписке
/// </summary>
public record GetSubscriptionInfoResponse
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; init; }
}