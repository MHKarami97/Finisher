using Microsoft.Extensions.Logging;
using Wolverine.Attributes;

namespace Finisher.Application.Behaviours;

public class UnhandledExceptionMiddleware(ILogger logger)
{
    [Middleware]
    public async Task HandleAsync(Func<Task> next)
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Messages.UnhandledException);

            throw;
        }
    }
}
