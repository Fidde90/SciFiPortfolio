using Microsoft.EntityFrameworkCore;
using SciFiPortfolio.Data.Context;
using SciFiPortfolio.Entities;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Models.ContentSections;

namespace SciFiPortfolio.Data.Seeders
{
    public class ReceptakutenFrontendPageSeeder
    {
        private readonly SciFiContext _context;
        private readonly ILogger<PageSeeder> _logger;

        public ReceptakutenFrontendPageSeeder(SciFiContext context, ILogger<PageSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var receptakuten = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten");
            var receptaktenFrontend = await _context.Pages.FirstOrDefaultAsync(x => x.Slug == "receptakuten-frontend");

            if (receptakuten is not null && receptaktenFrontend is null)
            {
                receptaktenFrontend = new PageEntity
                {
                    Title = "Frontend",
                    Slug = "receptakuten-frontend",
                    Published = true,
                    PublishedDate = DateTime.UtcNow,
                    ParentPageId = receptakuten.Id
                };

                _context.Pages.Add(receptaktenFrontend);
                await _context.SaveChangesAsync();
                _logger.LogInformation(":::::: Receptakuten Child Page - FrontEnd Seeded ::::::");
            }

            if (receptakuten is not null && receptaktenFrontend is not null)
            {
                var content = await _context.PageContents.FirstOrDefaultAsync(x => x.PageId == receptaktenFrontend.Id);

                if (content is null)
                {
                    var receptakutenFrontendContent = new PageContentEntity
                    {
                        PageId = receptaktenFrontend.Id,
                        Content =
                        {
                            Sections =
                            {
                                new HeroSection
                                {
                                    Heading = "Receptakuten",
                                    SubHeading = "Frontend",
                                    TextColor = "sci-fi-glow",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Frontenden är utvecklad i Vue.js 3 med Composition API och bygger på en komponentbaserad arkitektur där återanvändbarhet, " +
                                            "tydlig ansvarsfördelning och underhållbar kod har varit centrala designprinciper genom hela projektet."
                                        },
                                        new()
                                        {
                                            Text = "Applikationen består av ett stort antal specialiserade komponenter för bland annat recept, ingredienshantering, " +
                                            "gruppadministration, matscheman, profiler, formulär, dialogrutor och navigering. Genom att dela upp funktionaliteten i mindre " +
                                            "komponenter har samma komponenter kunnat återanvändas på flera ställen i applikationen, vilket minskar kodduplicering och förenklar " +
                                            "vidareutveckling."
                                        },
                                        new()
                                        {
                                            Text = "Affärslogiken hålls i stor utsträckning separerad från presentationslagret genom egna composables och serviceklasser. " +
                                            "Kommunikation mellan komponenter sker främst via props och emitters, medan globala händelser hanteras med en central EventBus. " +
                                            "Detta används exempelvis för att uppdatera olika delar av användargränssnittet när data förändras eller när notifieringar behöver visas " +
                                            "utan att komponenterna känner till varandra."
                                        },
                                        new()
                                        {
                                            Text = "Projektet är uppbyggt med en tydlig mappstruktur där komponenter, vyer, composables, API-tjänster och routing hålls åtskilda. " +
                                            "Detta gör projektet mer skalbart och enklare att underhålla " +
                                            "när nya funktioner läggs till."
                                        },
                                    },
                                    SpaceBottom = false,
                                    BordersCss = "border-top border-primary",
                                    BackgroundColorCss = "section-bg",
                                },
                                new TextSection
                                {
                                    Heading = "Routing och autentisering",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Navigeringen hanteras med Vue Router där olika layouts används beroende på användarens autentiseringsstatus. " +
                                            "Publika sidor, såsom inloggning, registrering och återställning av lösenord, använder en separat layout från den del " +
                                            "av applikationen som kräver inloggning. På så sätt hålls användarupplevelsen enkel innan användaren loggat in samtidigt " +
                                            "som den inloggade delen av applikationen kan erbjuda sidomenyer, navigering och användarspecifika funktioner."
                                        }
                                    },
                                    SpaceBottom = false,
                                },
                                new FlexSection
                                {
                                    ParagraphsLeft = [new() { Text = "Auth layout" }],
                                    ImagesLeft = [new() { ImageUrl = "images/auth-layout.png" }],
                                    ParagraphsRight = [new() { Text = "Account layout" }],
                                    ImagesRight = [new() { ImageUrl = "images/account-layout.png" }],
                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                },
                                new ImageWithPositionSection
                                {
                                    Heading = "Kommunikation med backend",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "All kommunikation med backend sker via ett REST-API genom ett separat servicelager. " +
                                            "Genom att samla API-anrop på ett ställe blir komponenterna enklare och mer fokuserade på presentation."
                                        },
                                        new()
                                        {
                                            Text = "För att förbättra prestandan lagras viss information lokalt i klienten för att minska mängden " +
                                            "upprepade API-anrop. Detta ger kortare laddningstider och gör att användaren upplever applikationen som " +
                                            "snabbare och mer responsiv."
                                        }
                                    },
                                    ImagePosition = ImagePosition.Bottom,
                                    DesktopImage = { ImageUrl = "images/api-call-ex.png" },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-top border-primary",
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Flera funktioner bygger på att affärslogiken ligger i backend istället för i klienten. Ett exempel är " +
                                            "genereringen av matscheman där backend ansvarar för slumpalgoritmen. Algoritmen ser till att samma recept inte " +
                                            "väljs flera gånger under samma vecka och att variationen ökar i takt med att fler recept finns tillgängliga. " +
                                            "Genom att placera denna logik i backend säkerställs att alla klienter får samma resultat och att reglerna inte " +
                                            "kan kringgås."
                                        },
                                        new() { Text = "Exempel på logiken i backend:" },
                                    },
                                    PaddingsCss = "",
                                    BackgroundColorCss = "section-bg",
                                    SpaceBottom = false,
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/part-off-dinner-schedule-creation.png" },
                                        new() { ImageUrl = "images/get-random-recipes.png" },
                                        new() { ImageUrl = "images/add-rotation-points.png" }
                                    },
                                    PaddingsCss = "pb-3",
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-bottom border-primary",
                                    SpaceBottom = false,
                                },
                                new ImageCenterSection
                                {
                                    Heading = "Realtidskommunikation",
                                    ParagraphsTop =
                                    {
                                        new()
                                        { Text = "Applikationen använder Server-Sent Events (SSE) för att skapa realtidsfunktionalitet. När användaren är inloggad " +
                                            "lyssnar klienten kontinuerligt efter händelser från servern utan att behöva göra återkommande polling-anrop."
                                        }
                                    },
                                    DesktopImages =
                                    {
                                        new() { ImageUrl = "images/sse-connect.png" },
                                        new() { ImageUrl = "images/sse-disconnect.png" }
                                    },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "Denna lösning används bland annat för gruppinbjudningar. När en användare blir inbjuden till en " +
                                            "grupp visas informationen direkt i gränssnittet utan att sidan behöver laddas om. " +
                                            "Detta bidrar till en mer responsiv och modern användarupplevelse."
                                        }
                                    },
                                    SpaceBottom = false,
                                },
                                new ListSection
                                {
                                    Heading = "Profilsida",
                                    Paragraphs = [new() { Text = "Varje användare har en profilsida där kontot kan administreras. Här kan användaren bland annat:" },],
                                    ListItems =
                                    {
                                        new() { Text = "ändra personlig information" },
                                        new() { Text = "byta lösenord" },
                                        new() { Text = "se hur många grupper man är medlem i" },
                                        new() { Text = "se hur många recept man har delat" },
                                        new() { Text = "anpassa applikationens tema" },
                                        new() { Text = "hantera övriga kontoinställningar" },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3",
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-top border-primary",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Genom att samla all kontohantering på ett ställe blir det enkelt för användaren " +
                                            "att administrera sitt konto."
                                        },
                                    },
                                    PaddingsCss = "pb-3",
                                    SpaceBottom = false,
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-bottom border-primary",
                                },
                                new TextSection
                                {
                                    Heading = "Användarupplevelse",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Stor vikt har lagts vid att skapa ett snyggt och responsivt gränssnitt. Datumhantering " +
                                            "sker med komponenten VueDatePicker, vilket ger ett betydligt mer användarvänligt " +
                                            "sätt att välja datum vid exempelvis planering av matscheman."
                                        },
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "pt-3",
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    [
                                        new() { ImageUrl = "images/datepicker.png" },
                                        new() { ImageUrl = "images/calendar.png" },
                                    ],
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Applikationen innehåller även automatisk konvertering av HEIC-bilder via Heic2any innan uppladdning. " +
                                            "Browser-image-compression används för att se till att bilden inte är förstor innan den skickas till backend, " +
                                            "men även backend kollar formatet, komprimerar och optimiserar bilden ytterligare så att det inte går att kringå i frontend." +
                                            "Detta gör att användare, framför allt från iPhone och andra Apple-enheter, " +
                                            "kan ladda upp receptbilder utan att själva behöva konvertera filformatet (för att det ska kunna visas i alla webbläsare)."
                                        },
                                        new() { Text = "För att göra applikationen mer personlig kan användaren även växla mellan olika teman." }
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    [
                                        new() { ImageUrl = "images/themes.png" },
                                    ],
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new TextSection
                                {
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Gränssnittet använder dessutom laddningsindikatorer vid längre operationer samt tydliga " +
                                            "återkopplingar när en åtgärd lyckas eller misslyckas. Detta gör att användaren alltid får tydlig " +
                                            "feedback på vad som händer."
                                        },
                                        new() { Text = "Applikationen är även responsivt uppbyggd och fungerar på både dator, surfplatta och mobiltelefon." }
                                    },
                                    SpaceBottom = false,
                                    PaddingsCss = "",
                                },
                                new ImageSection
                                {
                                    DesktopImages =
                                    [
                                        new() { ImageUrl = "images/receptakuten-mobile-desktop.png" },
                                    ],
                                    SpaceBottom = false,
                                    PaddingsCss = "pb-3",
                                },
                                new ImageCenterSection
                                {
                                    Heading = "Behörigheter och säkerhet",
                                    ParagraphsTop =
                                    {
                                        new()
                                        {
                                            Text = "Behörighetskontroller utförs alltid i backend. Frontenden använder den information som returneras " +
                                            "för att visa eller dölja funktioner beroende på användarens rättigheter, men alla kritiska kontroller verifieras " +
                                            "på serversidan."
                                        },
                                        new()
                                        {
                                            Text = "Exempelvis kan endast gruppens ägare bjuda in nya medlemmar eller ta bort befintliga medlemmar. Även om " +
                                            "någon skulle försöka manipulera klienten kommer backend att neka otillåtna åtgärder."
                                        }
                                    },
                                    DesktopImages = { new() { ImageUrl = "images/groupowner.png" }, },
                                    ParagraphsBottom =
                                    {
                                        new()
                                        {
                                            Text = "Åtkomst till receptbilder kontrolleras också av backend. Bilder kan endast visas av receptets ägare eller " +
                                            "av användare som är medlem i en grupp där receptet har delats."
                                        }
                                    },
                                    SpaceBottom = false,
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary"
                                },
                                new TextSection
                                {
                                    Heading = "Delning av recept",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "När ett recept delas inom en grupp skapas en separat entitet i backend istället för att flera användare " +
                                            "arbetar mot samma databaspost. " +
                                            "Denna lösning gör det möjligt för användare att skapa egna kopior av delade recept och fortsätta utveckla " +
                                            "dem utan att originalreceptet påverkas. " +
                                            "På så sätt kan recept delas mellan användare samtidigt som varje användare behåller möjligheten " +
                                            "att göra egna ändringar."
                                        },
                                    },
                                    SpaceBottom = false,
                                },
                                new TextSection
                                {
                                    Heading = "Kodkvalitet och skalbarhet",
                                    Paragraphs =
                                    {
                                        new()
                                        {
                                            Text = "Projektet har utvecklats med fokus på långsiktig underhållbarhet. Genom den komponentbaserade " +
                                            "arkitekturen, återanvändbara composables, ett separat API-lager och en tydlig projektstruktur är det " +
                                            "enkelt att lägga till nya funktioner utan att påverka befintlig kod."
                                        },
                                        new()
                                        {
                                            Text = "Sidor laddas dynamiskt via Vue Router (lazy loading) för att minska den initiala laddningstiden, " +
                                            "och återanvändbara komponenter används genom hela applikationen för att skapa ett enhetligt gränssnitt och " +
                                            "minska mängden duplicerad kod."
                                        },
                                        new()
                                        {
                                            Text = "Denna arkitektur har gjort det möjligt att successivt bygga ut projektet med nya funktioner utan att " +
                                            "behöva göra större förändringar i den befintliga kodbasen."
                                        },
                                    },
                                    BackgroundColorCss = "section-bg",
                                    BordersCss = "border-all border-primary",
                                    SpaceBottom = false,
                                },
                            }
                        }
                    };

                    _context.PageContents.Add(receptakutenFrontendContent);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation(":::::: Receptakuten Frontend PageContent Seeded ::::::");
                }
            }
        }
    }
}
