using Amazon.S3;

namespace Dal.File.YandexDisk;

/// <summary>
/// Обертка над <see cref="IAmazonS3"/> для прокидывания названия бакета
/// </summary>
public class YandexS3Client : IDisposable
{
    public IAmazonS3 InnerClient { get; }
    public string BucketName { get; }

    public YandexS3Client(AmazonS3Client innerClient, string bucketName)
    {
        InnerClient = innerClient;
        BucketName = bucketName;
    }

    public void Dispose()
    {
        InnerClient.Dispose();
    }
}