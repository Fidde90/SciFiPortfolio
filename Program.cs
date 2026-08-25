using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Data.Seeders;
using SciFiPortfolio.Interfaces;
using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Middlewares;
using SciFiPortfolio.Settings;
using SciFiPortfolio.Repositories;
using SciFiPortfolio.Services;

namespace SciFiPortfolio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IPageRepository, PageRepository>();
            builder.Services.AddScoped<IPageService, PageService>();
            builder.Services.AddScoped<IRendererService, RendererService>();
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
            builder.Services.AddScoped<ISeeder, PageSeeder>();
            builder.Services.AddScoped<ISeeder, AnalyticsSeeder>();
            builder.Services.AddScoped<DbSeeder>();
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
            });
            builder.Services.AddDbContext<SciFiContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
            ));
            builder.Services.Configure<CookieSettings>(
                builder.Configuration.GetSection("Cookies"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SciFiContext>();
                var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
                await context.Database.MigrateAsync();
                await seeder.SeedAsync();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");     
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseMiddleware<VisitorMiddleware>();
            app.UseAuthorization();
            app.MapControllers();
            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();
            app.Run();
        }
    }
}
