using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SciFiPortfolio.Enums;
using SciFiPortfolio.Models.ContentSections;
using SciFiPortfolio.ViewModels;

namespace SciFiPortfolio.Pages
{
    public class TestModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        public TestViewModel Vm { get; set; } = new();

        public TestModel(IWebHostEnvironment env) => _env = env;

        public void OnGet()
        {

            if (!_env.IsDevelopment())
                return;

            var sections = new List<ContentSection>()
            {
                //new HeroSection
                //{
                //    Image = new() { ImageUrl = "images/recept-bil-ub.png"  },
                //    Heading = "",
                //    SubHeading = "",
                //    SpaceBottom = true
                //},
               new ImageWithPositionSection
                {
                    Heading = "",
                    Paragraphs =
                    {
                        new()
                        { Text = "ReceptAkuten är en webbapp där användare kan skapa, spara och dela recept samt få hjälp med att planera sina måltider." +
                            "Målet med projektet är att göra det enklare att hålla ordning på sina recept och minska tiden som läggs på att planera veckans mat."
                        },
                        new()
                        { Text = "Användare kan skapa egna recept, spara recept från andra användare och anpassa kopior av recept efter egna önskemål utan att ändra originalet." +
                            "Det finns även möjlighet att skapa grupper där användare kan dela recept och innehåll med exempelvis familj eller vänner."
                        },
                         new()
                        { Text = "Systemet kan använda användarens valda recept för att skapa måltidsplaner och generera inköpslistor baserat på de planerade måltiderna." +
                            "Projektet har utvecklats med fokus på en tydlig struktur där presentation, affärslogik och datalagring är separerade för att göra systemet" +
                            "enklare att vidareutveckla och underhålla."
                        },
                    },
                    DesktopImage = { ImageUrl = "images/recept-bil-ub.png"},
                    ImagePosition = ImagePosition.Top,
                    BackgroundColor = "",

                },
                new ImageCenterSection
                {
                    Heading = "Systemarkitektur",
                    ParagraphsTop =
                    {
                        new()
                        {
                            Text = "Receptakuten är uppbyggt som en klient-server-applikation där frontend, backend och databas är separerade från varandra. Frontenden är utvecklad " +
                            "i Vue.js 3 med Composition API och kommunicerar med ett REST API byggt i .NET 8. Systemets data lagras i en PostgreSQL-databas."
                        },
                        new()
                        {
                            Text = " De olika delarna körs i separata Docker-containrar, vilket gör det enklare att utveckla och uppdatera varje del utan att påverka resten av systemet." +
                            "Containrarna kommunicerar med varandra via interna nätverk och hanteras som separata tjänster."
                        },
                    },
                    DesktopImages = { new() { ImageUrl = "images/receptakuten-arch-desktop.png" } },
                    MobileImages = { new() { ImageUrl = "images/receptakuten-arch-mobile.png" } },
                    ParagraphsBottom =
                    {
                        new()
                        {
                            Text = "I produktionsmiljön är frontenden tillgänglig via receptakuten.net och backend-API körs på en separat subdomän," +
                            "api.receptakuten.net. För att kunna testa nya funktioner innan de släpps finns även en separat stagingmiljö med" +
                            "egna instanser av frontend och backend. Denna miljö är skyddad med Basic Authentication och används för att kontrollera" +
                            "att förändringar fungerar innan de distribueras till produktion."
                        },
                        new()
                        {
                            Text = "Utvecklingen sker genom att nya funktioner först skapas i egna feature-brancher och sedan flyttas vidare genom" +
                            "olika miljöer innan de når produktion. Varje miljö har egna inställningar och miljövariabler för att separera" +
                            "testmiljön från produktionsmiljön och undvika påverkan på riktig användardata."
                        },
                    },
                    BackgroundColor = "section-bg",

                },
                new ImageWithPositionSection
                {
                    Heading = "Backend",
                    Paragraphs =
                    {
                        new()
                        {
                            Text = "Backenden är utvecklad enligt principerna för Clean Architecture i tankarna men följer inte fullt ut men försöker hålla ansvar och beroenden tydliga och " +
                            "separerade mellan olika lager (Bytte namn från Meal Menu till Receptakuten när projektet närmade sig sitt slut)."
                        },
                    },
                    DesktopImage = { ImageUrl = "images/receptakuten-layers.png"},
                    ImagePosition = ImagePosition.Bottom,
                    BackgroundColor = "",
                    SpaceBottom = false,
                    Paddings = false,

                },
                new ImageCenterSection
                {
                    ParagraphsTop =
                    {
                        new()
                        {
                            Text = "API-lagret fungerar som systemets yttersta gränssnitt och ansvarar för routing, autentisering, " +
                            "middleware, konfiguration av tjänster och mottagning av inkommande förfrågningar. Controllers innehåller " +
                            "minimalt med logik och fungerar främst som ett lager för validering och transformering av inkommande data."
                        },

                        new()
                        {
                            Text = "När en förfrågan når API:t valideras den först genom särskilda Request Models med egna regler " +
                            "för datavalidering. Därefter omvandlas informationen till DTO-objekt som skickas vidare till applikationslagret."
                        },
                    },
                    DesktopImages = { new() { ImageUrl = "images/ControllerDtoMapping.png" } },
                    ParagraphsBottom =
                    {
                        new()
                        {
                            Text = "Applikationslagret innehåller all affärslogik och är systemets kärna." +
                            "Här finns tjänster, DTO:er, entiteter, regler och processer som beskriver hur verksamheten fungerar." +
                            "Lagret är medvetet byggt utan beroenden till databaser, externa tjänster eller tekniska implementationer, " +
                            "vilket gör det enkelt att testa och vidareutveckla."
                        },
                        new()
                        {
                            Text = "Infrastructure-lagret ansvarar för den faktiska kommunikationen med databasen och externa system. " +
                            "Här finns repositories, Entity Framework-konfigurationer, filhantering och integrationer mot externa tjänster såsom Azure Communication Services. " +
                            "Genom att använda interfaces mellan lagren kan implementationer bytas ut utan att affärslogiken påverkas."
                        },
                    },
                    BackgroundColor = "",
                    SpaceBottom = false,
                    Paddings = false,
                },
                new ImageCenterSection
                {
                    SubHeading = "Databas och datamodell",
                    ParagraphsTop =
                    {
                         new()
                        {
                            Text = "Databasen är utvecklad enligt en Code First-strategi med Entity Framework Core. Datamodellen definieras " +
                            "i kod och migreras därefter automatiskt till PostgreSQL genom migrations. "
                        },
                         new()
                        {
                            Text = "Databasen innehåller ett flertal relationer mellan användare, grupper, recept, matscheman och inköpslistor. " +
                            "Denna struktur gör det möjligt att hantera både personliga receptsamlingar och gruppbaserade funktioner på ett konsekvent sätt. Ett exempel på relationer nedan:"
                        },
                    },
                    ParagraphsBottom =
                    {
                        new()
                        {
                            Text = "För att förbättra användarupplevelsen redan från start används seedning av data. " +
                            "Exempelrecept och grundläggande enheter för ingredienshantering skapas automatiskt när systemet initialiseras."
                        }
                    },
                    DesktopImages = { new() { ImageUrl = "images/recept-diagram.png" } },
                    BackgroundColor = "",
                    SpaceBottom = false,
                    Paddings = true,

                },
                new ListSection
                {
                    SubHeading = "Säkerhet",
                    Paragraphs =
                    [
                        new()
                        {
                            Text = "Säkerhet har varit en central del av projektets design."
                        },
                        new()
                        {
                            Text = "Autentisering sker genom ASP.NET Identity tillsammans med JWT-baserad autentisering. " +
                            "Access tokens och refresh tokens lagras i HttpOnly-cookies för att minska risken för exponering via klientskript."
                        },
                        new()
                        {
                            Text = "Autentisering sker genom ASP.NET Identity tillsammans med JWT-baserad autentisering. " +
                            "Access tokens och refresh tokens lagras i HttpOnly-cookies för att minska risken för exponering via klientskript."
                        },
                        new()
                        {
                            Text = "Systemet använder flera säkerhetslager för att skydda API:t, bland annat:"
                        }
                    ],
                    ListItems =
                    {
                        new() { Text = "JWT Authentication" },
                        new() { Text = "Refresh Tokens" },
                        new() { Text = "CSRF-skydd (XXS ?)" },
                        new() { Text = "Rate Limiting" },
                        new() { Text = "Inputvalidering" },
                        new() { Text = "Roll- och behörighetskontroller" },
                    },
                    SpaceBottom = false,
                    Paddings = false,

                },
                new TextSection
                {
                    Paragraphs =
                    {
                        new()
                        {
                            Text= "Utöver klientbaserade begränsningar verifieras samtliga rättigheter även på serversidan för att " +
                            "säkerställa att obehöriga användare inte kan kringgå systemets regler genom manipulerade anrop."
                        },
                        new()
                        {
                            Text= "För att aktivera nya konton krävs e-postverifiering och samma mekanism används vid glömt lösenord."
                        }
                    },
                    Paddings = false,
                    SpaceBottom = false,

                },
                new ImageWithPositionSection
                {
                    SubHeading = "Realtidsfunktioner",
                    Paragraphs =
                    {
                        new()
                        {
                            Text = "För funktioner som kräver omedelbar återkoppling används Server-Sent Events (SSE). " +
                            "Ett exempel är gruppinbjudningar där mottagaren får uppdateringar direkt från servern utan att " +
                            "klienten behöver skicka återkommande förfrågningar.",
                        },
                        new()
                        {
                            Text = "Detta ger snabbare återkoppling samtidigt som belastningen på API:t minskar jämfört med traditionell polling.",
                        }
                    },
                    DesktopImage = { ImageUrl = "images/inbjudning.png" },
                    ImagePosition = ImagePosition.Bottom,
                    BackgroundColor = "",
                    SpaceBottom = false,
                    Paddings = true,

                },
                new ImageCenterSection
                {
                    SubHeading = "Bildhantering",
                    ParagraphsTop =
                    {
                        new()
                        {
                            Text="Användaruppladdade bilder bearbetas på serversidan med hjälp av ImageMagick via Magick.NET. Där kontroll av accepterade bildtypr kontrolleras, storlek osv"
                        },

                    },
                    DesktopImages = {new() { ImageUrl= "images/allowed-images.png" } },
                    ParagraphsBottom =
                    {
                        new()
                        {
                            Text="Vid uppladdning genomförs konvertering och storlek/kvallitets optimering för att minska lagringsutrymme och förbättra laddningstider. " +
                            "Bilderna lagras därefter på servern och kopplas till respektive recept. Nedan kan man se att Clean Code mönster följs med flera små metoder " +
                            "används för att ge klarare förståelse när man läser koden, detta upprepas över hela projektet."
                        },
                    },
                    BackgroundColor = "",
                    SpaceBottom = false,
                    Paddings = false,
                },
                new ImageSection
                {
                    DesktopImages =
                    {
                        new() { ImageUrl = "images/save-image.png"}
                    },
                    BackgroundColor = "",
                    Paddings = false
                },
                new ListSection
                {
                    SubHeading = "Automatisering och bakgrundsjobb",
                    Paragraphs =
                    [
                        new() { Text = "Systemet använder Quartz.NET för schemalagda bakgrundsjobb som körs oberoende av användaraktivitet." },
                        new() { Text = "Dessa jobb används bland annat för att:" }
                    ],
                    ListItems =
                    {
                        new() { Text = "Hantera avslutade matscheman" },
                        new() { Text = "Rensa utgångna refresh tokens" },
                        new() { Text = "Identifiera inaktiva användare" },
                        new() { Text = "Ta bort konton som aldrig aktiverats" },
                    },
                    SpaceBottom = true,
                    Paddings = false,
                },
                new TextSection
                {
                    Paragraphs =
                    {
                        new() { Text = "Dessa jobb configureras med hjälp av nycklar och triggers där man sedan anger när de ska köras, Här kan ni se ett exempel på ett av dess jobb: " },
                    },
                    Paddings = false,
                    SpaceBottom = false,
                },
                new FlexSection
                {
                    ImagesLeft = [ new () { ImageUrl = "images/quartz-conf-2.png" } ],
                    ImagesRight = [ new () { ImageUrl = "images/quartz-job-ex-3.png" } ],
                    SpaceBottom = false,
                    Paddings = false
                },
                new TextSection
                {
                    Paragraphs =
                    {
                        new() 
                        { 
                            Text= "Genom att flytta denna typ av arbete till separata processer kan API:t fokusera" +
                            " på användarrelaterade förfrågningar samtidigt som återkommande underhåll sker automatiskt." 
                        },
                    },
                    Paddings = false,
                    SpaceBottom = false,
                },
                new TextSection
                {
                    SubHeading = "Loggning och felhantering",
                    Paragraphs =
                    {
                        new() 
                        { 
                            Text= "För övervakning och felsökning används Serilog med stöd för både konsolloggning och filbaserad loggning."
                        },
                        new()
                        {
                            Text= " Systemet använder en central Global Exception Handler som fångar upp oväntade fel och returnerar konsekventa felmeddelanden till klienten. " +
                            "Detta minskar mängden duplicerad felhantering i applikationen och förenklar felsökning."
                        },
                        new()
                        {
                            Text= "I särskilda situationer där återhämtning är möjlig används riktade try/catch-block för att hantera specifika undantag," +
                            "exempelvis vid filhantering och borttagning av resurser från servern."
                        },
                    },
                    Paddings = true,
                    SpaceBottom = true,
                },
                new TextSection
                {
                    Heading = "Frontend",
                    Paragraphs =
                    {
                        new()
                        {
                            Text="Frontenden är utvecklad i Vue.js 3 med Composition API och bygger på en komponentbaserad arkitektur där " +
                            "återanvändbarhet och tydlig ansvarsfördelning har varit centrala designprinciper."
                        },
                        new()
                        {
                            Text=" Applikationen består av ett stort antal specialiserade komponenter för recept, ingredienshantering, " +
                            "gruppadministration, matscheman, formulär och dialogrutor. Kommunikation mellan komponenter sker genom props, " +
                            "emitters och gemensamma eventflöden, vilket möjliggör en tydlig separation mellan presentation och affärslogik."
                        },new()
                        {
                            Text="Navigeringen hanteras med Vue Router där olika layouts används beroende på användarens autentiseringsstatus. " +
                            "Publika sidor såsom inloggning och registrering använder en separat layout från de delar av applikationen som kräver inloggning."
                        },new()
                        {
                            Text="För att förbättra användarupplevelsen lagras utvald data lokalt i klienten för att minska mängden upprepade API-anrop. " +
                            "Detta bidrar till snabbare laddningstider och en mer responsiv användarupplevelse."
                        },
                        new()
                        {
                            Text="Klienten innehåller även stöd för automatisk konvertering av bildformat såsom HEIC innan uppladdning, " +
                            "vilket säkerställer kompatibilitet mellan olika enheter och webbläsare."
                        }
                    },
                    BackgroundColor = "section-bg"
                },
            };

            Vm.Sections.AddRange(sections);

        }
    }
}
