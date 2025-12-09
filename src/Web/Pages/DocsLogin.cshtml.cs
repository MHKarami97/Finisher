using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security.Claims;
using Finisher.Shared.Consts;
using Finisher.Shared.Resources;
using Finisher.Web.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Finisher.Web.Pages;

public class DocsLoginModel(IOptions<DocsAuthOptions> optionsAccessor) : PageModel
{
    private readonly DocsAuthOptions _options = optionsAccessor.Value;

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
            ModelState.AddModelError(string.Empty, Messages.NotValidValue);
            return Page();
        }

        var version = $"{_options.Username}:{_options.Password}";

        if (_options.ExpireEveryWeek)
        {
            var now = DateTimeOffset.UtcNow;
            var utcDate = now.UtcDateTime;
            var isoWeek = ISOWeek.GetWeekOfYear(utcDate);
            var isoYear = ISOWeek.GetYear(utcDate);
            var weekId = $"{isoYear:D4}-W{isoWeek:D2}";
            version += $":{weekId}";
        }

        var versionHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(version)));
        var claims = new[] { new Claim(ClaimTypes.Name, Username), new Claim(DocConsts.DocsAccess, "Allowed"), new Claim(DocConsts.DocsConfigVersion, versionHash) };

        var identity = new ClaimsIdentity(claims, DocConsts.DocsCookie);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            DocConsts.DocsCookie,
            principal,
            new AuthenticationProperties { IsPersistent = true });

        return LocalRedirect(DocConsts.ScalarAddress);
    }

    public async Task<IActionResult> OnPostLogoutAsync()
    {
        await HttpContext.SignOutAsync(DocConsts.DocsCookie);
        return RedirectToPage();
    }
}
