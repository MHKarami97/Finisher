using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

internal class ConfirmEmail(IMessageBus bus) : Endpoint<ConfirmEmailCommand>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(ConfirmEmail));
        AllowAnonymous();
        Summary(s => s.Summary = nameof(ConfirmEmail));
    }

    public override async Task HandleAsync(ConfirmEmailCommand req, CancellationToken ct)
    {
        await bus.InvokeAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
