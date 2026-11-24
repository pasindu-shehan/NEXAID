//using System.ComponentModel.DataAnnotations;

//namespace backend.Models
//{
//    public class ComplaintEmbedding
//    {
//        public int ComplaintId { get; set; }
//        public Complaints Complaint { get; set; } = null!;
//        public int Id { get; set; }

//        public string EmbeddingJson { get; set; }

//        public string ModelName { get; set; }

//        public DateTime CreatedAt { get; set; }
//    }
//}


namespace backend.Models
{
    public class ComplaintEmbedding
    {
        public int Id { get; set; }

        public int ComplaintId { get; set; }

        public Complaints Complaint { get; set; } = null!;

        public string EmbeddingJson { get; set; } = null!;

        public string ModelName { get; set; } = "all-MiniLM-L6-v2";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}