namespace MT.Contracts.Common;

/// <summary>
/// Unified format for error responses.
/// </summary>
public class ApiErrorResponse
{
    public bool Succeeded { get; set; } = false;
    public List<ErrorDetail> Errors { get; set; } = new();
}

/// <summary>
/// Single error entry (code and message).
/// </summary>
public class ErrorDetail
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
