using Microsoft.Extensions.Options;
using Finisher.Application.Business.Logger.Commands;
using Finisher.Web.Configurations;

namespace Finisher.Web.Endpoints.Logger;

public class CreateLoggerEndpoint(IMessageBus bus, IOptionsMonitor<UiLogger> uiLoggerOption) : BaseCreateEndPoint<LoggerCreateCommand>(bus, ApiName.Logger)
{
    public override void Configure()
    {
        Post(ApiName.Logger);
        Tags(ApiTag.Create);
        Summary(s => s.Summary = SummaryCreator.Create(ApiName.Logger));
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoggerCreateCommand req, CancellationToken ct)
    {
        if (uiLoggerOption.CurrentValue.IsEnabled)
        {
            await Bus.InvokeAsync(req, ct);
        }
    }
}
