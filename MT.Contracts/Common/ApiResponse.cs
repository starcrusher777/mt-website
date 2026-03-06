namespace MT.Contracts.Common;

/// <summary>
/// Unified format for successful API responses.
/// </summary>
public class ApiResponse<T>
{
    public bool Succeeded { get; set; } = true;
    public T? Data { get; set; }
}
