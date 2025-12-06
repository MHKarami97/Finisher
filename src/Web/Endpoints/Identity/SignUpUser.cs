using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

internal class SignUpUser(IMessageBus bus) : Endpoint<SignUpUserCommand>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(SignUpUser));
        AllowAnonymous();
        Summary(s => s.Summary = nameof(SignUpUser));
    }

    public override async Task HandleAsync(SignUpUserCommand req, CancellationToken ct)
    {
        await bus.InvokeAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
