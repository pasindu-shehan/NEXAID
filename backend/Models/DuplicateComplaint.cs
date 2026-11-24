using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class DuplicateComplaint
    {
        public int Id { get; set; }

        public int OriginalComplaintId { get; set; }

        public int DuplicateComplaintId { get; set; }

        public double SimilarityScore { get; set; }

        public string ReviewStatus { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; }

        public Complaints OriginalComplaint { get; set; } = null!;
        public Complaints DuplicateComplaintDetails { get; set; } = null!;

    }
}
