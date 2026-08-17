using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Data.Seeders
{
    public class VpsPageSeeder
    {
        private readonly SciFiContext _context;
        private readonly ILogger<PageSeeder> _logger;

        public VpsPageSeeder(SciFiContext context, ILogger<PageSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var vpsHosting = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "vps-hosting");

            if (vpsHosting is null)
            {
                vpsHosting = new PageEntity
                {
                    Title = "Serverarkitektur",
                    Slug = "vps-hosting",
                    Published = true,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Pages.Add(vpsHosting);
                await _context.SaveChangesAsync();

                _logger.LogInformation(":::::: Vps Page Seeded ::::::");
            }

            if (vpsHosting is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == vpsHosting.Id);

                if (content is null)
                {
                    var vpsHostingPageContent = new PageContentEntity
                    {
                        PageId = vpsHosting.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Image = new() { ImageUrl = "images/server.png" },
                                    SpaceBottom = true,
                                    PaddingsCss = "",
                                },
                                new ListSection
                                {
                                    SubHeading = "Teknikstack",
                                    Paragraphs = new()
                                    {
                                        new()
                                        {
                                            Text = "Jag designade och driftsatte en containerbaserad servermiljö på en VPS-hostad Ubuntu Server. " +
                                            "Infrastrukturens fokus ligger på isolering, säkerhet och enkel skalning av applikationer."
                                        }
                                    },
                                    ListItems = {
                                        new() { Text = "Ubuntu Server (minimal installation)", IconName = "" },
                                        new() { Text = "Docker & Docker Compose", IconName = "" },
                                        new() { Text = "Nginx Reverse Proxy", IconName = "" },
                                        new() { Text = "PostgreSQL", IconName = "" },
                                        new() { Text = "Certbot (Let´s Encrypt)", IconName = "" },
                                        new() { Text = "VPS-hosting via Hostinger)", IconName = "" },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                                new ImageWithPositionSection
                                {
                                    Heading = "Infrastrukturdesign",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Servern är uppbyggd enligt en lagerindelad arkitektur där Nginx körs direkt på " +
                                            "värdmaskinen och fungerar som central ingresspunkt för all inkommande trafik."
                                        },
                                    },
                                    DesktopImage = { ImageUrl = "images/infra-desktop.png" },
                                    MobileImage = { ImageUrl = "images/infra-mobile.png" },
                                    ImagePosition = ImagePosition.Bottom,
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Nätverksarkitektur",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Servermiljön är uppbyggd kring en central ingressmodell där Nginx fungerar som reverse proxy " +
                                            "för samtliga applikationer. Genom att hantera SSL, domänrouting och trafikstyrning på värdservern skapas en tydlig " +
                                            "separation mellan publika och interna resurser."
                                        },

                                        new()
                                        {
                                            Text = "Applikationerna körs i isolerade Docker-nätverk och exponeras inte direkt mot internet. " +
                                            "Istället routas inkommande trafik till rätt container baserat på domän och konfiguration. Lösningen ger en " +
                                            "flexibel grund för att hosta flera tjänster på samma server samtidigt som säkerhet, underhållbarhet och skalbarhet bibehålls."
                                        }
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Säkerhet",
                                    Paragraphs = {
                                        new()
                                        {
                                            Text = "Säkerhet har varit en central del av serverarkitekturen redan från början. " +
                                            "All extern trafik krypteras med SSL-certifikat från Let's Encrypt som automatiskt förnyas med hjälp av Certbot. " +
                                            "Nginx fungerar som den enda publika ingresspunkten och ansvarar för att dirigera trafiken vidare till rätt tjänst."
                                        },

                                        new()
                                        {
                                            Text = "Databaser exponeras aldrig direkt mot internet utan är endast åtkomliga från de " +
                                            "containrar som behöver kommunicera med dem. Genom privata Docker-nätverk och separerade applikationsmiljöer " +
                                            "skapas ytterligare ett skyddslager som begränsar åtkomsten mellan olika system."
                                        }
                                    },
                                   SpaceBottom = false,
                                },
                                new DockerSection()
                                {
                                    Heading = "Containerisering",
                                    LeftParagraph =
                                    {
                                        Text = "Säkerhet har varit en central del av serverarkitekturen redan från början. All extern trafik krypteras med " +
                                        "SSL-certifikat från Let's Encrypt som automatiskt förnyas med hjälp av Certbot. Nginx fungerar som den enda publika " +
                                        "ingresspunkten och ansvarar för att dirigera trafiken vidare till rätt tjänst. Databaser exponeras aldrig " +
                                            "direkt mot internet utan är endast åtkomliga från de containrar som behöver kommunicera med dem. Genom privata " +
                                            "Docker-nätverk och separerade applikationsmiljöer skapas ytterligare ett skyddslager som begränsar åtkomsten mellan olika system."
                                    },

                                    RightParagraph =
                                    {
                                        Text = "För att skapa en stabil och lättadministrerad driftmiljö är plattformen uppbyggd kring Docker." +
                                        "Varje applikation körs i sin egen isolerade container tillsammans med en dedikerad PostgreSQL-databas. Genom att separera tjänsterna " +
                                        "från varandra minimeras risken att ett problem i en applikation påverkar övriga system på servern."
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Skalbarhet",
                                    Paragraphs =
                                    {
                                        new()
                                        { Text = "Infrastrukturen är designad för att enkelt kunna växa när nya projekt eller tjänster " +
                                            "behöver läggas till. Genom Docker Compose definieras hela miljön som kod, vilket innebär att samma " +
                                            "konfiguration kan återskapas på nya servrar med minimala manuella insatser."
                                        },

                                        new()
                                        {
                                            Text = "När en ny applikation ska driftsättas skapas en separat containeruppsättning med egna miljövariabler, " +
                                            "databasresurser och nätverksinställningar. Nginx kan därefter konfigureras för att routa trafik till den nya tjänsten " +
                                            "utan att påverka redan existerande applikationer."
                                        },

                                        new()
                                        {
                                            Text = "Detta arbetssätt ger konsekventa miljöer mellan utveckling, test och produktion samtidigt " +
                                            "som det förenklar både felsökning och framtida expansion."
                                        },
                                    },
                                    SpaceBottom = false,
                                },
                                new TextSection()
                                {
                                    Heading = "Resultat",
                                    Paragraphs = {
                                        new()
                                        {
                                            Text = "Den färdiga plattformen ger en robust grund för att hosta flera applikationer på samma " +
                                            "server utan att kompromissa med säkerhet eller underhållbarhet. Genom containerisering, centraliserad trafikhantering " +
                                            "och automatiserad certifikathantering har infrastrukturen blivit både enkel att administrera och enkel att vidareutveckla."
                                        },

                                        new()
                                        {
                                            Text = "Arkitekturen möjliggör snabb driftsättning av nya projekt samtidigt som befintliga tjänster kan uppdateras " +
                                            "oberoende av varandra. Resultatet är en flexibel och skalbar plattform som kan växa i takt med nya behov utan att kräva större " +
                                            "förändringar i den underliggande infrastrukturen."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(vpsHostingPageContent);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation(":::::: Vps Hosting PageContent Seeded ::::::");
                }
            }
        }
    }
}
