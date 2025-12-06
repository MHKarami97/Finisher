using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

public class ForgotPassword(IMessageBus bus) : Endpoint<ForgotPasswordCommand>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(ForgotPassword));
        AllowAnonymous();
        Summary(s => s.Summary = nameof(ForgotPassword));
    }

    public override async Task HandleAsync(ForgotPasswordCommand req, CancellationToken ct)
    {
        await bus.InvokeAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
