using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Models.ContentSections;
using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Interfaces;

namespace SciFiPortfolio.Data.Seeders
{
    public class PageSeeder : ISeeder
    {
        private readonly SciFiContext _context;

        public PageSeeder(SciFiContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await SeedPagesAsync();
        }

        private async Task SeedPagesAsync()
        {
            if(!await _context.Pages.AnyAsync(page => page.Slug == "/"))
            {
                var indexPage = new PageEntity
                {
                    Title = "",
                    Slug = "/",
                    Published = true,
                    PageContent =
                {
                    Sections =
                    {
                        new TextSection()
                        {
                            Paragraphs =
                            {
                                new(){ Text = "Hej! Jag är en .NET Fullstack-utvecklare med ett särskilt intresse för backendutveckling. Jag tycker om att bygga API:er, designa databaser och skapa robusta system som är skalbara, säkra och enkla att underhålla." },
                                new(){ Text = "Även om backend är där jag känner mig mest hemma har jag också erfarenhet av frontendutveckling med Vue.js, JavaScript, HTML och CSS. Det gör att jag kan ta ansvar för hela utvecklingsprocessen – från databas och API till användargränssnitt och användarupplevelse." },
                                new(){ Text = "Jag arbetar regelbundet med Docker för containerisering, Nginx för webbserver- och proxyhantering samt Ubuntu Server för drift och deployment. Kombinationen av utveckling och infrastruktur ger mig en helhetsförståelse för hur moderna webbapplikationer byggs, distribueras och underhålls i produktion." },
                                new(){ Text = "Jag drivs av att lösa problem, lära mig nya tekniker och utveckla lösningar som skapar verkligt värde. Oavsett om det handlar om att optimera prestanda i ett API, bygga nya funktioner eller sätta upp en stabil produktionsmiljö strävar jag alltid efter att leverera kod av hög kvalitet." },
                                new(){ Text = "När jag inte utvecklar utforskar jag gärna nya tekniker och verktyg för att fortsätta utvecklas som utvecklare och hålla mig uppdaterad inom branschen." }
                            },
                            BackgroundColor = true
                        },
                        new CardSection()
                        {
                            Cards =
                            {
                                new()
                                {
                                    Title = "SPA Applikation",
                                    Image = { ImageUrl = "Images/receptakuten.png", AltText = "receptakuten image"},
                                    Link = { LinkText= "Testa den här", LinkUrl="https://receptakuten.net", Icon = new(){ IconText = "fa-solid fa-arrow-right" } },
                                    LinkButton = { LinkText="Läs mer om appen", LinkUrl="/Projects/ReceptakutenPage" },
                                    Tags = {
                                        new(){ TagText= ".Net API" },
                                        new(){ TagText = "Vue.js" },
                                        new(){ TagText = "PostgreSql" },
                                        new(){ TagText = "Identity" }
                                    }
                                },
                                new()
                                {
                                    Title = "Vps hosting",
                                    Image = { ImageUrl = "Images/server.png", AltText = "image of a server"},
                                    LinkButton = { LinkText="Läs mer", LinkUrl="Projects/ServerPage" },
                                    Tags = {
                                        new(){ TagText= "NginX" },
                                        new(){ TagText = "Ubuntu Server" },
                                        new(){ TagText = "Docker" },
                                        new(){ TagText = "Hostinger" }
                                    }
                                },
                                new()
                                {
                                    Title = "Sci-fi portfolio",
                                    Image = { ImageUrl = "Images/portimg.jpeg", AltText = "a sci-fi image"},
                                    LinkButton = { LinkText="Läs mer", LinkUrl="/" },
                                    Tags = {
                                        new(){ TagText= "Razor pages" },
                                        new(){ TagText = "Vanilla javascript" },
                                        new(){ TagText = "SSR" },
                                    }
                                }
                            }
                        }
                    }
                }
                };

                _context.Pages.Add(indexPage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
