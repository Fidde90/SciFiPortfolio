using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;

namespace SciFiPortfolio.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(IAnalyticsRepository analyticsRepository, ILogger<AnalyticsService> logger)
        {
            _analyticsRepository = analyticsRepository;
            _logger = logger;
        }

        public async Task<int> NewVisitAsync()
        {
            var result = await _analyticsRepository.IncrementVisitorCountAsync();

            if (result > 0)
            {
                var time = DateTime.UtcNow.ToShortDateString();
                _logger.LogInformation(":::::::::::::::::::::::::::: New Visitor at: {0} :::::::::::::::::::::::", time);
            }

            return result;
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
