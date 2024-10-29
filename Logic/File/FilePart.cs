using Destructurama.Attributed;

namespace Logic.File;

public record FilePart
{
    public Guid FileId { get; init; }
    
    [LogMasked(Text = "*FILE STREAM*")]
    public Stream FileStream { get; init; }
}