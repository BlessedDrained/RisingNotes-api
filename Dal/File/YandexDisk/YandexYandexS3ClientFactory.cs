using Amazon.Runtime;
using Amazon.S3;

namespace Dal.File.YandexDisk;

/// <inheritdoc />
public class YandexYandexS3ClientFactory : IYandexS3ClientFactory
{
    private readonly BasicAWSCredentials _credentials;
    private readonly AmazonS3Config _config;
    private readonly string _bucketName;
    
    public YandexYandexS3ClientFactory(IConfiguration configuration)
    {
        const string Location = "ru-central1";
        const string Url = "https://s3.yandexcloud.net";
        
        var section = configuration.GetRequiredSection("S3Credentials");
        var accessKeyId = section.GetValue<string>("AccessKeyId");
        var accessKey = section.GetValue<string>("AccessKey");
        _bucketName = section.GetValue<string>("BucketName");
        _credentials = new BasicAWSCredentials(accessKey: accessKeyId, secretKey: accessKey);
        _config = new AmazonS3Config()
        {
            ServiceURL = Url,
            AuthenticationRegion = Location
        };
    }

    /// <inheritdoc />
    public YandexS3Client CreateClient()
    {
        return new YandexS3Client(new AmazonS3Client(_credentials, _config), _bucketName);
    }
}