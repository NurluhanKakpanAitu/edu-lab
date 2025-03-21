namespace Infrastructure.File;

/// <summary>
/// Configuration settings for MinIO.
/// </summary>
public class MinioConfig
{
    /// <summary>
    /// Gets or sets the endpoint for the MinIO server.
    /// Example: "http://localhost:9000"
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the access key for MinIO authentication.
    /// Example: "minio-access-key"
    /// </summary>
    public string AccessKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the secret key for MinIO authentication.
    /// Example: "minio-secret-key"
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the default bucket name where files will be stored.
    /// Example: "mybucket"
    /// </summary>
    public string DefaultBucket { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether to use secure (HTTPS) or insecure (HTTP) connection.
    /// </summary>
    public bool UseSSL { get; set; } = false;

    /// <summary>
    /// Gets or sets the region of the MinIO storage (if applicable).
    /// </summary>
    public string Region { get; set; } = string.Empty;
}