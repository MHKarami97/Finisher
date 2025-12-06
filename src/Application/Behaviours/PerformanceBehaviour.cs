using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Finisher.Application.Interfaces.User;
using Wolverine.Attributes;

namespace Finisher.Application.Behaviours;

public class PerformanceMiddleware<T>(ILogger<T> logger, IUser user)
{
    private const int WarningThresholdMilliseconds = 500;

    [Middleware]
    public async Task HandleAsync(T message, Func<Task> next)
    {
        var stopwatch = Stopwatch.StartNew();

        await next();

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > WarningThresholdMilliseconds)
        {
            var requestName = typeof(T).Name;
            var userId = user.IdIfLoggedIn;

            logger.LogWarning(
                "Finisher Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                requestName, stopwatch.ElapsedMilliseconds, userId, message
            );
        }
    }
}
