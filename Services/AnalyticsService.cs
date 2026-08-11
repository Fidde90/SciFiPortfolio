using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;

namespace SciFiPortfolio.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public async Task<int> NewVisitAsync()
        {
            return await _analyticsRepository.IncrementVisitorCountAsync();
        }

        public CookieOptions CreateCookie(DateTimeOffset duration, SameSiteMode sameSiteMode, bool secure, bool httpOnly)
        {
            return new CookieOptions
            {
                Path = "/",
                Secure = secure,
                HttpOnly = httpOnly,
                Expires = duration,
                SameSite = sameSiteMode
            };
        }
    }
}
