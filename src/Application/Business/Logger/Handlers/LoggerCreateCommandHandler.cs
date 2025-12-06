using Finisher.Application.Business.Logger.Commands;
using Microsoft.Extensions.Logging;

namespace Finisher.Application.Business.Logger.Handlers;

public class LoggerCreateCommandHandler
{
    public void HandleAsync(LoggerCreateCommand command, ILogger<LoggerCreateCommandHandler> logger)
    {
        logger.LogWarning("UI Log: {@Command}", command);
    }
}
