namespace Finisher.Web.Middlewares;

internal class RequestLogger<TRequest> : IPreProcessor<TRequest>
{
    public Task PreProcessAsync(IPreProcessorContext<TRequest> context, CancellationToken ct)
    {
        var logger = context.HttpContext.Resolve<ILogger<TRequest>>();

        logger.LogInformation("request:{FullName} path: {RequestPath}", context.Request?.GetType().FullName, context.HttpContext.Request.Path);

        return Task.CompletedTask;
    }
}
