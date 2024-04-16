using MainLib.CustomException;
using MainLib.Enums;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

public class S3FileDownloadException : BadRequestException
{

    public S3FileDownloadException(StorageType storageType) : base($"Error occured while trying to download file from {storageType}", RisingNotesErrorConstants.FileDownload)
    {
    }
}