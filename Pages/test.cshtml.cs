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
                new HeroSection
                {
                    Heading = "Receptakuten - Frontend",
                    TextColor = "sci-fi-glow"
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
                    SpaceBottom = true,
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
                    BackgroundColor = "section-bg-grey",

                    SpaceBottom = false,
                },
                new FlexSection
                {
                    ParagraphsLeft =
                    [
                       new(){ Text = "Auth layout" }
                    ],
                    ImagesLeft =
                    [
                        new() { ImageUrl = "images/auth-layout.png" }
                    ],
                    ParagraphsRight =
                    [
                       new(){ Text = "Account layout" }
                    ],
                    ImagesRight =
                    [
                        new() { ImageUrl = "images/account-layout.png" }
                    ],
                    BackgroundColor = "section-bg-grey",
                    Paddings = "pb-3"
                },
                new ImageWithPositionSection
                {
                    Paragraphs =
                    {
                        new()
                        {
                            Text = "Glömt lösenord har implementerats som ett komplett återställningsflöde med egna sidor där " +
                            "användaren först begär en återställning via e-post och därefter väljer ett nytt lösenord genom en säker verifieringslänk." +
                            "Backend ansvarar för autentisering och behörighetskontroller och sedan skickas e-post via Azure Communication Service " +
                            "medan frontenden anpassar gränssnittet efter användarens rättigheter."
                        },
                    },
                    ImagePosition = ImagePosition.Right,
                    DesktopImage = { ImageUrl= "" },
                    BackgroundColor ="section-bg-purple",
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
                    DesktopImage = { ImageUrl= "images/api-call-ex.png" },
                    BackgroundColor ="",
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
                        new()
                        {
                            Text = "Exempel på logiken i backend:"
                        },
                    },
                    Paddings = "",
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
                    Paddings = "pb-3"
                }
            };

            Vm.Sections.AddRange(sections);
        }
    }
}
