using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Finisher.Shared.Consts;
using Finisher.Web.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Finisher.Web.Pages;

public class SwaggerLoginModel(IOptions<SwaggerAuthOptions> optionsAccessor) : PageModel
{
    private readonly SwaggerAuthOptions _options = optionsAccessor.Value;

    [Required]
    [BindProperty]
    public required string Username { get; set; }

    [Required]
    [BindProperty]
    public required string Password { get; set; }

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
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return Page();
        }

        var version = $"{_options.Username}:{_options.Password}";
        var versionHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(version)));
        var claims = new[] { new Claim(ClaimTypes.Name, Username), new Claim(MainConsts.SwaggerAccess, "Allowed"), new Claim(MainConsts.SwaggerConfigVersion, versionHash) };

        var identity = new ClaimsIdentity(claims, MainConsts.SwaggerCookie);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            MainConsts.SwaggerCookie,
            principal,
            new AuthenticationProperties { IsPersistent = true });

        return LocalRedirect(MainConsts.Swagger);
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(MainConsts.SwaggerCookie);
        return RedirectToPage();
    }
}
