namespace SciFiPortfolio.Interfaces.Services
{
    public interface IAnalyticsService
    {
        Task<int> NewVisitAsync();
        CookieOptions CreateCookie(DateTimeOffset duration, SameSiteMode sameSiteMode, bool secure, bool httpOnly);
    }
}
