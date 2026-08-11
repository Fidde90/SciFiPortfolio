using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SciFiPortfolio.Constants;
using SciFiPortfolio.Settings;

namespace SciFiPortfolio.Controllers
{
    public class CookieController : Controller
    {
        private readonly IOptions<CookieSettings> _cookieSettings;

        public CookieController(IOptions<CookieSettings> cookieSettings)
        {
            _cookieSettings = cookieSettings;
        }

        [Route("/cookie-consent")]
        [ValidateAntiForgeryToken]
        public IActionResult CookieConsent(string consent)
        {
            if (string.IsNullOrWhiteSpace(consent) || consent is not (CookieConsentOptions.Accepted or CookieConsentOptions.Declined))
                return BadRequest();

            var cookieSettings = _cookieSettings.Value;

            var options = new CookieOptions
            {
                Path = "/",
                SameSite = SameSiteMode.Lax,
                HttpOnly = false,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            };

            Response.Cookies.Append(cookieSettings.Consent, consent, options);

            return RedirectToPage("/Index");
        }
    }
}
