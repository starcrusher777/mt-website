using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MT.Contracts.Common;

namespace MerchTrade.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var value in context.ActionArguments.Values)
        {
            if (value == null) continue;
            var valueType = value.GetType();
            var validatorType = typeof(IValidator<>).MakeGenericType(valueType);
            var validator = _serviceProvider.GetService(validatorType);
            if (validator == null) continue;

            var validateMethod = validatorType.GetMethod("ValidateAsync", new[] { valueType, typeof(CancellationToken) });
            if (validateMethod == null) continue;

            var task = (Task<FluentValidation.Results.ValidationResult>)validateMethod.Invoke(validator, new[] { value, CancellationToken.None })!;
            var result = await task;
            if (result.IsValid) continue;

            var response = new ApiErrorResponse
            {
                Errors = result.Errors
                    .Select(e => new ErrorDetail { Code = e.ErrorCode, Message = e.ErrorMessage })
                    .ToList()
            };
            context.Result = new BadRequestObjectResult(response);
            return;
        }

        await next();
    }
}
