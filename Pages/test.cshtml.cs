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
                //    Heading = "Receptakuten",
                //    SubHeading = "",
                //    SpaceBottm = true
                //},
               new ImageWithPositionSection
                {
                    Heading = "Om projektet",
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
                    ImagePosition = ImagePosition.Right,
                    BackgroundColor = "section-bg",

                },
                new ImageWithPositionSection
                {
                    Heading = "Systemarkitektur",
                    Paragraphs =
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
                    DesktopImage = { ImageUrl = "images/receptakuten-arch-desktop.png"},
                    MobileImage = { ImageUrl = "images/receptakuten-arch-mobile.png" },
                    ImagePosition = ImagePosition.Bottom,
                    BackgroundColor = "",

                },
                 new TextSection()
                        {
                            Heading = "Backendarkitektur",
                            Paragraphs =
                            {
                                new(){ Text = "Backenden är utvecklad enligt principerna för Clean Architecture där ansvar och beroenden är tydligt separerade mellan olika lager." },
                                new(){ Text = "API-lagret fungerar som systemets yttersta gränssnitt och ansvarar för routing, autentisering, middleware, konfiguration av tjänster och mottagning av inkommande förfrågningar. Controllers innehåller minimalt med logik och fungerar främst som ett lager för validering och transformering av inkommande data." },
                                new(){ Text = "När en förfrågan når API:t valideras den först genom särskilda Request Models med egna regler för datavalidering. Därefter omvandlas informationen till DTO-objekt som skickas vidare till applikationslagret." },
                                new(){ Text = "Applikationslagret innehåller all affärslogik och är systemets kärna. Här finns tjänster, DTO:er, entiteter, regler och processer som beskriver hur verksamheten fungerar. Lagret är medvetet byggt utan beroenden till databaser, externa tjänster eller tekniska implementationer, vilket gör det enkelt att testa och vidareutveckla." },
                                new(){ Text = "Infrastructure-lagret ansvarar för den faktiska kommunikationen med databasen och externa system. Här finns repositories, Entity Framework-konfigurationer, filhantering och integrationer mot externa tjänster såsom Azure Communication Services. Genom att använda interfaces mellan lagren kan implementationer bytas ut utan att affärslogiken påverkas." }
                            },
                            BackgroundColor = "section-bg",
                        },
            };

            Vm.Sections.AddRange(sections);

        }
    }
}
