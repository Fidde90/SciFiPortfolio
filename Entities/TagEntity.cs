using System.ComponentModel.DataAnnotations;

namespace SciFiPortfolio.Entities
{
    public class TagEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Text { get; set; } = "";


        public ICollection<ProjectCardEntity> Cards { get; set; } = [];
    }
}
