namespace SciFiPortfolio.Models
{
    public class AppLink
    {
        public string LinkText { get; set; } = "";

        public RouteValues RouteValues { get; set; } = new();

        public Icon? Icon {  get; set; }
    }
}
