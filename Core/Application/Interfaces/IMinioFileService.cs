namespace Application.Interfaces;

public interface IMinioFileService
{
    /// <summary>
    /// Uploads a file to MinIO.
    /// </summary>
    /// <param name="objectName">The name of the object (file) to save in MinIO.</param>
    /// <param name="data">The file stream to be uploaded.</param>
    /// <param name="contentType">The content type of the file, e.g., "image/png".</param>
    /// <returns>The public URL of the uploaded file.</returns>
    Task<string?> UploadFileAsync(string objectName, Stream data, string contentType);

    /// <summary>
    /// Downloads a file from MinIO.
    /// </summary>
    /// <param name="objectName">The name of the object (file) to download from MinIO.</param>
    /// <returns>The file contents as a byte array.</returns>
    Task<byte[]> DownloadFileAsync(string objectName);
}