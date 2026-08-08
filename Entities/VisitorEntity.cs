using System.ComponentModel.DataAnnotations;

namespace SciFiPortfolio.Entities
{
    public class VisitorEntity
    {
        [Key]
        public int Id { get; set; }

        public int VisitorCount { get; set; }
    }
}
