namespace Application;

/// <summary>
/// A generic class for representing standard API responses.
/// </summary>
/// <typeparam name="T">The type of data returned in the response.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// The data returned from the operation (if successful).
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Creates a success response with data.
    /// </summary>
    /// <param name="data">The data to return in the response.</param>
    /// <returns>A success ApiResponse instance.</returns>
    public static ApiResponse<T?> CreateSuccess(T? data)
    {
        return new ApiResponse<T?>
        {
            Data = data,
            Success = true,
            ErrorMessage = null
        };
    }

    /// <summary>
    /// Creates a failure response with an error message.
    /// </summary>
    /// <param name="errorMessage">Error details for the response.</param>
    /// <returns>A failure ApiResponse instance.</returns>
    public static ApiResponse<T> CreateFailure(string errorMessage)
    {
        return new ApiResponse<T>
        {
            Data = default,
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}