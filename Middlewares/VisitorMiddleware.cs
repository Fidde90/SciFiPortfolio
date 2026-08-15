using Microsoft.Extensions.Options;
using SciFiPortfolio.Constants;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Settings;

namespace SciFiPortfolio.Middlewares
{
    public class VisitorMiddleware
    {
        public RequestDelegate _next;

        public VisitorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAnalyticsService analyticsService, IOptions<CookieSettings> cookieSettings)
        {
            var endPoint = context.GetEndpoint();

            if (context.Request.Method == "GET" && !string.IsNullOrWhiteSpace(endPoint?.DisplayName))
            {
                var settings = cookieSettings.Value;
                var cookieConsent = context.Request.Cookies[settings.Consent];

                if (cookieConsent == CookieConsentOptions.Accepted && !context.Request.Cookies.ContainsKey(settings.Visitor))
                {
                    int visitResult = await analyticsService.NewVisitAsync();

                    if (visitResult > 0)
                    {
                        context.Response.Cookies.Append(settings.Visitor, "true",
                            analyticsService.CreateCookie(DateTimeOffset.UtcNow.AddSeconds(10), SameSiteMode.Lax, true, true));
                    }
                }
            }

            await _next(context);
        }
    }
}
