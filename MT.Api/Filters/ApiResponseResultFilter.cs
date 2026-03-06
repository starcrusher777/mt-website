using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MT.Contracts.Common;

namespace MerchTrade.Filters;

/// <summary>
/// Wraps successful responses in the unified ApiResponse format.
/// </summary>
public class ApiResponseResultFilter : IAsyncResultFilter
{
    private static readonly Type ApiResponseType = typeof(ApiResponse<>);

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is OkObjectResult okResult && okResult.Value != null)
        {
            var valueType = okResult.Value.GetType();
            if (!IsAlreadyWrapped(valueType))
            {
                var responseType = ApiResponseType.MakeGenericType(valueType);
                var response = Activator.CreateInstance(responseType);
                responseType.GetProperty("Data")!.SetValue(response, okResult.Value);
                context.Result = new OkObjectResult(response);
            }
        }

        await next();
    }

    private static bool IsAlreadyWrapped(Type valueType)
    {
        return valueType.IsGenericType && valueType.GetGenericTypeDefinition() == ApiResponseType;
    }
}
