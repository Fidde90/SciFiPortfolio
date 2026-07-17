using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Models.ContentSections;
using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Interfaces;
using SciFiPortfolio.Enums;

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
            if (!await _context.Pages.AnyAsync(page => page.Slug == "/"))
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
                        new HeroSection
                        {
                            Heading = "Fredrik Bengtsson",
                            SubHeading = "Fullstack .Net Utvecklare",
                            SpaceBottm = true
                        },
                        new CarouselSection
                        {
                            Images =
                            {
                                new(){ ImageUrl="Images/vue.svg", AltText= "vue icon"},
                                new(){ ImageUrl="Images/javascript.svg", AltText= "javascript icon"},
                                new(){ ImageUrl="Images/net.svg", AltText= ".net icon"},
                                new(){ ImageUrl="Images/csharp.svg", AltText= "c# icon"},
                                new(){ ImageUrl="Images/azure.svg", AltText= "azure icon"},
                                new(){ ImageUrl="Images/api.svg", AltText= "api icon"},
                                new(){ ImageUrl="Images/docker.svg", AltText= "docker icon"},
                                new(){ ImageUrl="Images/nginx.svg", AltText= "nginX icon"}
                            },
                        },
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

            if (!await _context.Pages.AnyAsync(page => page.Slug == "vps-hosting-2"))
            {
                var serverPage_2 = new PageEntity
                {
                    Title = "Serverarkitektur",
                    Slug = "vps-hosting-2",
                    Published = true,
                    PageContent =
                {
                    Sections =
                    {
                        new HeroSection
                        {
                            Image = new(){ ImageUrl = "Images/server.png"},
                            SpaceBottm = true
                        },

                        new TextSection
                        {
                            Paragraphs = {
                                new(){ Text = "Jag designade och driftsatte en containerbaserad servermiljö på en VPS-hostad Ubuntu Server. Infrastrukturens fokus ligger på isolering, säkerhet och enkel skalning av applikationer." }
                            },
                            BackgroundColor = true,
                            SpaceBottm = false
                        },
                        new ListSection
                        {
                            Heading = "Teknikstack",
                            ListItems = {
                                new(){Text="Ubuntu Server (minimal installation)", Icon = new(){ IconText = "" } },
                                new(){Text="Docker & Docker Compose", Icon = new(){ IconText = "" }},
                                new(){Text="Nginx Reverse Proxy", Icon = new(){ IconText = "" }},
                                new(){Text="PostgreSQL", Icon = new(){ IconText = "" }},
                                new(){Text="Certbot (Let´s Encrypt)", Icon = new(){ IconText = "" }},
                                new(){Text="VPS-hosting via Hostinger)", Icon = new(){ IconText = "" }},
                            },
                            BackgroundColor = true,
                        },
                        new ImageWithPositionSection
                        {
                            Heading = "Infrastrukturdesign",
                            Paragraphs =
                            {
                                new(){ Text = "Servern är uppbyggd enligt en lagerindelad arkitektur där Nginx körs direkt på värdmaskinen och fungerar som central ingresspunkt för all inkommande trafik." },
                            },
                            Image = { ImageUrl = "images/infra2.png"},
                            ImagePosition = ImagePosition.Bottom
                        },
                        new TextSection()
                        {
                            Heading = "Nätverksarkitektur",
                            Paragraphs = {
                                new(){ Text = "Servermiljön är uppbyggd kring en central ingressmodell där Nginx fungerar som reverse proxy för samtliga applikationer. Genom att hantera SSL, domänrouting och trafikstyrning på värdservern skapas en tydlig separation mellan publika och interna resurser." },
                                new(){ Text = "Applikationerna körs i isolerade Docker-nätverk och exponeras inte direkt mot internet. Istället routas inkommande trafik till rätt container baserat på domän och konfiguration. Lösningen ger en flexibel grund för att hosta flera tjänster på samma server samtidigt som säkerhet, underhållbarhet och skalbarhet bibehålls." }
                            },
                            BackgroundColor = true
                        },
                        new TextSection()
                        {
                            Heading = "Säkerhet",
                            Paragraphs = {
                                new(){ Text = "Säkerhet har varit en central del av serverarkitekturen redan från början. All extern trafik krypteras med SSL-certifikat från Let's Encrypt som automatiskt förnyas med hjälp av Certbot. Nginx fungerar som den enda publika ingresspunkten och ansvarar för att dirigera trafiken vidare till rätt tjänst." },
                                new(){ Text = "Databaser exponeras aldrig direkt mot internet utan är endast åtkomliga från de containrar som behöver kommunicera med dem. Genom privata Docker-nätverk och separerade applikationsmiljöer skapas ytterligare ett skyddslager som begränsar åtkomsten mellan olika system." }
                            },
                        },
                        new TextSection()
                        {
                            Heading = "Skalbarhet",
                            Paragraphs = {
                                new(){ Text = "Infrastrukturen är designad för att enkelt kunna växa när nya projekt eller tjänster behöver läggas till. Genom Docker Compose definieras hela miljön som kod, vilket innebär att samma konfiguration kan återskapas på nya servrar med minimala manuella insatser." },
                                new(){ Text = "När en ny applikation ska driftsättas skapas en separat containeruppsättning med egna miljövariabler, databasresurser och nätverksinställningar. Nginx kan därefter konfigureras för att routa trafik till den nya tjänsten utan att påverka redan existerande applikationer." },
                                new(){ Text = "Detta arbetssätt ger konsekventa miljöer mellan utveckling, test och produktion samtidigt som det förenklar både felsökning och framtida expansion." },
                            },
                        },
                        new TextSection()
                        {
                            Heading = "Resultat",
                            Paragraphs = {
                                new(){ Text = "Den färdiga plattformen ger en robust grund för att hosta flera applikationer på samma server utan att kompromissa med säkerhet eller underhållbarhet. Genom containerisering, centraliserad trafikhantering och automatiserad certifikathantering har infrastrukturen blivit både enkel att administrera och enkel att vidareutveckla." },
                                new(){ Text = "Arkitekturen möjliggör snabb driftsättning av nya projekt samtidigt som befintliga tjänster kan uppdateras oberoende av varandra. Resultatet är en flexibel och skalbar plattform som kan växa i takt med nya behov utan att kräva större förändringar i den underliggande infrastrukturen." },
                            },
                            BackgroundColor = true,
                            SpaceBottm = false
                        },
                    }
                }
                };

                _context.Pages.Add(serverPage_2);
                await _context.SaveChangesAsync();
            }
        }
    }
}
