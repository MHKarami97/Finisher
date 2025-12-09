using Finisher.Application.Exceptions;
using Finisher.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace Finisher.Web.Extensions;

abstract class CustomExceptionHandler;

internal static class ExceptionHandlerExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app,
        ILogger? logger = null,
        bool logStructuredException = false,
        bool showExceptionDetail = false)
    {
        app.UseExceptionHandler(errApp => errApp.Run(async ctx =>
        {
            var feature = ctx.Features.Get<IExceptionHandlerFeature>();
            if (feature is null)
            {
                return;
            }

            logger ??= ctx.Resolve<ILogger<CustomExceptionHandler>>();

            var ex = feature.Error;

            string type;
            string reason;
            string userAction;
            int httpCode;

            switch (ex)
            {
                case ValidationException vex:
                    httpCode = StatusCodes.Status400BadRequest;
                    type = ExceptionInfo.Error400.Type;
                    reason = vex.Message;
                    userAction = ExceptionInfo.Error400.UserAction;
                    break;

                case ArgumentException:
                case FormatException:
                case InvalidOperationException:
                    httpCode = StatusCodes.Status400BadRequest;
                    type = ExceptionInfo.Error400.Type;
                    reason = ExceptionInfo.Error400.Reason;
                    userAction = ExceptionInfo.Error400.UserAction;
                    break;

                case SecurityTokenExpiredException:
                case SecurityTokenInvalidSignatureException:
                case SecurityTokenException:
                    httpCode = StatusCodes.Status401Unauthorized;
                    type = ExceptionInfo.Error401.Type;
                    reason = ExceptionInfo.Error401.Reason;
                    userAction = ExceptionInfo.Error401.UserAction;
                    break;

                case UnauthorizedAccessException:
                    httpCode = StatusCodes.Status403Forbidden;
                    type = ExceptionInfo.Error403.Type;
                    reason = ExceptionInfo.Error403.Reason;
                    userAction = ExceptionInfo.Error403.UserAction;
                    break;

                case KeyNotFoundException:
                    httpCode = StatusCodes.Status404NotFound;
                    type = ExceptionInfo.Error404.Type;
                    reason = ExceptionInfo.Error404.Reason;
                    userAction = ExceptionInfo.Error404.UserAction;
                    break;

                default:
                    httpCode = StatusCodes.Status500InternalServerError;
                    type = ExceptionInfo.Error500.Type;
                    reason = ExceptionInfo.Error500.Reason;
                    userAction = ExceptionInfo.Error500.UserAction;
                    break;
            }

            var route = feature.Endpoint?.DisplayName?.Split(" => ")[0];
            var exceptionType = ex.GetType().Name;

            if (logStructuredException)
            {
                logger.LogStructuredException(ex, exceptionType, route, reason);
            }
            else
            {
                logger.LogUnStructuredException(exceptionType, route, reason, ex.StackTrace);
            }

            ctx.Response.StatusCode = httpCode;
            ctx.Response.ContentType = "application/problem+json";

            var response = new ApiErrorResponse
            {
                Type = type,
                Code = httpCode,
                UserAction = userAction,
                Reason = reason,
                Exception = showExceptionDetail ? ex : null
            };

            await ctx.Response.WriteAsJsonAsync(response, ctx.RequestAborted);
        }));

        return app;
    }
}

internal sealed class ApiErrorResponse
{
    public required string Type { get; init; }
    public required int Code { get; init; }
    public required string Reason { get; init; }
    public string? UserAction { get; init; }
    public Exception? Exception { get; init; }
}
