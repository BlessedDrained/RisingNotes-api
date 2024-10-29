using Amazon.Runtime;
using Amazon.S3;

namespace Dal.File.S3;

/// <inheritdoc />
public class S3ClientFactory : IS3ClientFactory
{
    private readonly BasicAWSCredentials _credentials;
    private readonly AmazonS3Config _config;
    private readonly string _bucketName;

    public S3ClientFactory(IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("S3Credentials");
        var accessKeyId = section.GetValue<string>("AccessKeyId");
        var accessKey = section.GetValue<string>("AccessKey");
        var s3Url = section.GetValue<string>("S3Url");
        _bucketName = section.GetValue<string>("BucketName");
        
        _credentials = new BasicAWSCredentials(accessKey: accessKeyId, secretKey: accessKey);
        _config = new AmazonS3Config()
        {
            ServiceURL = s3Url,
        };
    }

    /// <inheritdoc />
    public S3Client CreateClient()
    {
        return new S3Client(new AmazonS3Client(_credentials, _config), _bucketName);
    }
}