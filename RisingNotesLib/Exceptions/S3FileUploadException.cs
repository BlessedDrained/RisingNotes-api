using MainLib.CustomException;
using MainLib.Enums;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Ошибка загрузки файла в s3 хранилище
/// </summary>
public class S3FileUploadException : BadRequestException
{
    public S3FileUploadException(StorageType storageType) 
        : base($"Error occured while trying to upload file to {storageType}", RisingNotesErrorConstants.FileUpload)
    {
        
    }
    
    public S3FileUploadException(string message) 
        : base(message, RisingNotesErrorConstants.FileUpload)
    {
    }
}