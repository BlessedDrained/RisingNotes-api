using Dal.File;
using Dal.ShortVideo;
using Dal.ShortVideo.Repository;
using Logic.File;
using MainLib.Logging;
using MainLib.TagLib;

namespace Logic.ShortVideo;

/// <inheritdoc />
public class ShortVideoManager : IShortVideoManager
{
    private readonly IShortVideoRepository _musicClipRepository;
    private readonly IFileManager _fileManager;

    public ShortVideoManager(
        IShortVideoRepository musicClipRepository,
        IFileManager fileManager)
    {
        _musicClipRepository = musicClipRepository;
        _fileManager = fileManager;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadAsync(ShortVideoDal clip, FileDal clipFile, FileDal previewFile)
    {
        using var log = new MethodLog(clip);

        var file = TagLib.File.Create(new FileAbstraction($"{clipFile.Name}.{clipFile.Extension}", clipFile.Content));
        clip.DurationMsec = Convert.ToInt32(file.Properties.Duration.TotalMilliseconds);

        await _fileManager.UploadAsync(clipFile);
        await _fileManager.UploadAsync(previewFile);

        clip.VideoFileId = clipFile.Id;
        clip.PreviewFileId = previewFile.Id;
        var id = await _musicClipRepository.InsertAsync(clip);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<ShortVideoDal> GetAsync(Guid id)
    {
        using var log = new MethodLog(id);

        var clip = await _musicClipRepository.GetAsync(id);

        log.ReturnsValue(clip);
        return clip;
    }

    /// <inheritdoc />
    public async Task<FileDal> GetPreviewAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        // await using var transaction = await _musicClipRepository.BeginTransactionOrExistingAsync();

        var clip = await _musicClipRepository.GetAsync(clipId);
        var file = await _fileManager.DownloadAsync(clip.PreviewFileId);

        log.ReturnsValue(file);
        return file;
    }

    public async Task<FileDal> GetFileAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        // await using var transaction = await _musicClipRepository.BeginTransactionOrExistingAsync();

        var clip = await _musicClipRepository.GetAsync(clipId);
        var file = await _fileManager.DownloadAsync(clip.VideoFileId);

        log.ReturnsValue(file);
        return file;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(ShortVideoDal clip)
    {
        using var log = new MethodLog(clip);

        // await using var transaction = await _musicClipRepository.BeginTransactionOrExistingAsync();
        
        await _musicClipRepository.UpdateAsync(clip);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        using var log = new MethodLog(id);
        
        // await using var transaction = await _musicClipRepository.BeginTransactionOrExistingAsync();
        
        await _musicClipRepository.DeleteAsync(id);
    }
}