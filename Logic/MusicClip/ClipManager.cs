using Dal.File;
using Dal.MusicClip;
using Dal.MusicClip.Repository;
using Logic.File;
using MainLib.Logging;
using MainLib.TagLib;

namespace Logic.MusicClip;

/// <inheritdoc />
public class ClipManager : IClipManager
{
    private readonly IClipRepository _clipRepository;
    private readonly IFileManager _fileManager;

    public ClipManager(
        IClipRepository clipRepository,
        IFileManager fileManager)
    {
        _clipRepository = clipRepository;
        _fileManager = fileManager;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadAsync(ClipDal clip, FileDal clipFile, FileDal previewFile)
    {
        using var log = new MethodLog(clip);

        var file = TagLib.File.Create(new FileAbstraction($"{clipFile.Name}.{clipFile.Extension}", clipFile.Content));
        clip.DurationMsec = Convert.ToInt32(file.Properties.Duration.TotalMilliseconds);

        await _fileManager.UploadAsync(clipFile);
        await _fileManager.UploadAsync(previewFile);
        
        clip.ClipFileId = clipFile.Id;
        clip.PreviewFileId = previewFile.Id;
        var id = await _clipRepository.InsertAsync(clip);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<ClipDal> GetAsync(Guid id)
    {
        using var log = new MethodLog(id);

        var clip = await _clipRepository.GetAsync(id);

        log.ReturnsValue(clip);
        return clip;
    }

    /// <inheritdoc />
    public async Task<FileDal> GetPreviewAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        // await using var transaction = await _clipRepository.BeginTransactionOrExistingAsync();

        var clip = await _clipRepository.GetAsync(clipId);
        var file = await _fileManager.DownloadAsync(clip.PreviewFileId);

        log.ReturnsValue(file);
        return file;
    }

    public async Task<FileDal> GetFileAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        // await using var transaction = await _clipRepository.BeginTransactionOrExistingAsync();

        var clip = await _clipRepository.GetAsync(clipId);
        var file = await _fileManager.DownloadAsync(clip.ClipFileId);

        log.ReturnsValue(file);
        return file;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(ClipDal clip)
    {
        using var log = new MethodLog(clip);

        // await using var transaction = await _clipRepository.BeginTransactionOrExistingAsync();
        
        await _clipRepository.UpdateAsync(clip);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        using var log = new MethodLog(id);
        
        // await using var transaction = await _clipRepository.BeginTransactionOrExistingAsync();
        
        await _clipRepository.DeleteAsync(id);
    }
}