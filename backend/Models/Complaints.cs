using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Complaints
    {
  
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Category { get; set; }

        public string? Status { get; set; }

        public string? Location { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ComplaintEmbedding? ComplaintEmbedding { get; set; }

        public ICollection<DuplicateComplaint> OriginalDuplicates { get; set; }
            = new List<DuplicateComplaint>();

        public ICollection<DuplicateComplaint> DuplicateOf { get; set; }
            = new List<DuplicateComplaint>();
}
}
