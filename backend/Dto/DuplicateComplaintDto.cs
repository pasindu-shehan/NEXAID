namespace backend.Dto
{
    public class DuplicateComplaintDto
    {
        public int ComplaintId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Location { get; set; }

        public double SimilarityScore { get; set; }
    }
}
