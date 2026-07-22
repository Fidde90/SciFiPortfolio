using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Data.Seeders;
using SciFiPortfolio.Interfaces;
using SciFiPortfolio.Interfaces.Repositories;
using SciFiPortfolio.Interfaces.Services;
using SciFiPortfolio.Repositories;
using SciFiPortfolio.Services;

namespace SciFiPortfolio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SciFiContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
            ));

            builder.Services.AddScoped<IContentSectionRepository, ContentSectionRepository>();
            builder.Services.AddScoped<IContentSectionService, ContentSectionService>();
            
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IImageService, ImageService>();
            
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();

            builder.Services.AddScoped<IPageRepository, PageRepository>();
            builder.Services.AddScoped<IPageService, PageService>();

            builder.Services.AddScoped<IRendererService, RendererService>();

            builder.Services.AddScoped<ISeeder, PageSeeder>();
            builder.Services.AddScoped<DbSeeder>();

            builder.Services.AddRazorPages();

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseQueryStrings = true;
                options.LowercaseUrls = true;
            });

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SciFiContext>();
                await context.Database.MigrateAsync();

                var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
                await seeder.SeedAsync();
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
