namespace Api.Controllers.Subscription.Dto.Response;

/// <summary>
/// Ответ на получение списка подписок пользователя
///  /// </summary>
public record GetSubscriptionListResponse
{
    /// <summary>
    /// Список подписок
    /// </summary>
    public List<GetSubscriptionInfoResponse> SubscriptionList { get; init; } = new();
}