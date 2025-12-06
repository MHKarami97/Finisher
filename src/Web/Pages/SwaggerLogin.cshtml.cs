using System.Security.Claims;
using Finisher.Shared.Consts;
using Finisher.Web.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Finisher.Web.Pages;

internal sealed class SwaggerLogin(IOptions<SwaggerAuthOptions> optionsAccessor) : PageModel
{
    private readonly SwaggerAuthOptions _options = optionsAccessor.Value;

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; } = MainConsts.Swagger;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var isValidUser =
            string.Equals(Username, _options.Username, StringComparison.Ordinal) &&
            string.Equals(Password, _options.Password, StringComparison.Ordinal);

        if (!isValidUser)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, Username),
            new Claim("SwaggerAccess", "Allowed")
        };

        var identity = new ClaimsIdentity(claims, "SwaggerCookie");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            "SwaggerCookie",
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true
            });

        var target = string.IsNullOrWhiteSpace(ReturnUrl) ? MainConsts.Swagger : ReturnUrl;

        return LocalRedirect(target);
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync("SwaggerCookie");
        return RedirectToPage();
    }
}
