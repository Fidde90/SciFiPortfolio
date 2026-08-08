namespace SciFiPortfolio.Middlewares
{
    public class VisitorMiddleware
    {
        public RequestDelegate _next;

        public VisitorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET" && !Path.HasExtension(context.Request.Path))
            {
                bool HasCookie = context.Request.Cookies.ContainsKey("vsd");

                if (!HasCookie)
                {
                    var options = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTime.UtcNow.AddMinutes(60)
                    };

                    context.Response.Cookies.Append("vsd", "true", options);
                }
            }

            await _next(context);
        }
    }
}
