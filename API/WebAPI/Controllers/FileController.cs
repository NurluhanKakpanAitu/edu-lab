using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class FileController(IMinioFileService minioFileService) : BaseController
{
    /// <summary>
    /// Uploads a file to MinIO storage.
    /// </summary>
    /// <param name="file">The file to upload.</param>
    /// <returns>An ApiResponse containing the public URL of the uploaded file or an error message.</returns>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(ApiResponse<string>.CreateFailure("Invalid file."));
        }

        try
        {
            await using var stream = file.OpenReadStream();
            var objectName = $"{Guid.NewGuid()}_{file.FileName}";

            var response = await minioFileService.UploadFileAsync(objectName, stream, file.ContentType);

            return ResponseOk(response); 
        }
        catch (Exception ex)
        {
            return ResponseFail($"An error occurred: {ex.Message}");
        }
        
        
    }

    /// <summary>
    /// Downloads a file from MinIO storage.
    /// </summary>
    /// <param name="objectName">The name of the file to download.</param>
    /// <returns>An ApiResponse containing the file data as a byte array or an error message.</returns>
    [HttpGet("download/{objectName}")]
    public async Task<IActionResult> DownloadFile([FromRoute] string objectName)
    {
        try
        {
            var response = await minioFileService.DownloadFileAsync(objectName);

            return File(response, "application/octet-stream", objectName);
        }
        catch (Exception ex)
        {
            return ResponseFail($"An error occurred: {ex.Message}");
        }
    }
    
    [HttpGet("base64/{objectName}")]
    public async Task<IActionResult> DownloadFileAsBase64([FromRoute] string objectName)
    {
        try
        {
            var response = await minioFileService.DownloadFileAsync(objectName);

            return ResponseOk(response);
        }
        catch (Exception ex)
        {
            return ResponseFail($"An error occurred: {ex.Message}");
        }
    }
}