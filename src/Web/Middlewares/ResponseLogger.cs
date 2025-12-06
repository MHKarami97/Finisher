namespace Finisher.Web.Middlewares;

internal class ResponseLogger<TRequest, TResponse> : IPostProcessor<TRequest, TResponse>
{
    public Task PostProcessAsync(IPostProcessorContext<TRequest, TResponse> context, CancellationToken ct)
    {
        var logger = context.HttpContext.Resolve<ILogger<TResponse>>();

        logger.LogInformation("response:{FullName} path: {RequestPath}", context.Response?.GetType().FullName, context.HttpContext.Request.Path);

        return Task.CompletedTask;
    }
}
