using Finisher.Application.Business.Identity.Commands;

namespace Finisher.Web.Endpoints.Identity;

public class VerifySignInCodeUser(IMessageBus bus) : Endpoint<VerifySignInCodeUserCommand, string>
{
    public override void Configure()
    {
        Post(ApiName.Identity.AddSeparator() + nameof(VerifySignInCodeUser));
        AllowAnonymous();
        Summary(s => s.Summary = nameof(VerifySignInCodeUser));
    }

    public override async Task HandleAsync(VerifySignInCodeUserCommand req, CancellationToken ct)
    {
        var token = await bus.InvokeAsync<string>(req, ct);
        await Send.OkAsync(token, ct);
    }
}
