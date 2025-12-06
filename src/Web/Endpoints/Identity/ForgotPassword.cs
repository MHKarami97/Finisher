using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

internal class ForgotPassword(IMessageBus bus) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(ForgotPassword));
        AllowAnonymous();
        Summary(s => s.Summary = nameof(ForgotPassword));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await bus.InvokeAsync(new ForgotPasswordCommand(), ct);
        await Send.NoContentAsync(ct);
    }
}
