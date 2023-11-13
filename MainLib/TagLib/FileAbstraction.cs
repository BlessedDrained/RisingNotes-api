using File = TagLib.File;

namespace MainLib.TagLib;

/// <inheritdoc />
public class FileAbstraction : File.IFileAbstraction
{
    public FileAbstraction(string fileName, byte[] content)
    {
        Name = fileName;

        var stream = new MemoryStream(content);
        ReadStream = stream;
        WriteStream = stream;
    }

    /// <inheritdoc />
    public void CloseStream(Stream stream)
    {
        stream.Dispose();
    }

    /// <inheritdoc />
    public string Name { get; private set; }

    /// <inheritdoc />
    public Stream ReadStream { get; private set; }

    /// <inheritdoc />
    public Stream WriteStream { get; private set; }
}