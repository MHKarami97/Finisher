using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

public class SignInUser(IMessageBus bus) : Endpoint<SignInUserCommand>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(SignInUser));
        AllowAnonymous();
        Summary(s => s.Summary = nameof(SignInUser));
    }

    public override async Task HandleAsync(SignInUserCommand req, CancellationToken ct)
    {
        await bus.InvokeAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
