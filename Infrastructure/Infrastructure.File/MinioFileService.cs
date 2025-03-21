using Application.Interfaces;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace Infrastructure.File;

public class MinioFileService(
    IMinioClient minioClient,
    IOptions<MinioConfig> options) : IMinioFileService
{
    private readonly MinioConfig _minioConfig = options.Value;

    /// <inheritdoc />
    public async Task<string?> UploadFileAsync(string objectName, Stream data, string contentType)
    {
        try
        {
            // Ensure the bucket exists
            var bucketExists =
                await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_minioConfig.DefaultBucket));
            if (!bucketExists)
            {
                await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_minioConfig.DefaultBucket));
            }

            // Upload the file with metadata
            var response = await minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(_minioConfig.DefaultBucket)
                .WithObject(objectName)
                .WithStreamData(data)
                .WithObjectSize(data.Length)
                .WithContentType(contentType));

            return response.ObjectName;
        }
        catch (MinioException ex)
        {
            Console.WriteLine($"[MinIO Error] Upload Failed: {ex.Message}");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<byte[]> DownloadFileAsync(string objectName)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            
            await minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_minioConfig.DefaultBucket)
                .WithObject(objectName)
                .WithCallbackStream(stream => { stream.CopyTo(memoryStream); }));
            
            return memoryStream.ToArray();
        }
        catch (MinioException ex)
        {
            Console.WriteLine($"[MinIO Error] Download Failed: {ex.Message}");
            throw;
        }
    }
}