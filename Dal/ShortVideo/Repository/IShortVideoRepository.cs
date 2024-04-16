using MainLib.Dal.Repository.Base;

namespace Dal.ShortVideo.Repository;

/// <summary>
/// Репозиторий для коротких видео
/// </summary>
public interface IShortVideoRepository : IRepository<ShortVideoDal, Guid>
{
    
}