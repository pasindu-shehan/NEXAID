//using backend.Models;
//using Microsoft.EntityFrameworkCore;

//namespace backend.Repositories
//{
//    public class ComplaintEmbeddingRepository
//    {
//        public async Task SaveEmbeddingAsync(
//    int complaintId,
//    float[] embedding)
//        {
//            var complaintEmbedding = new ComplaintEmbedding
//            {
//                ComplaintId = complaintId,

//                EmbeddingJson = System.Text.Json.JsonSerializer.Serialize(embedding),

//                ModelName = "all-MiniLM-L6-v2",

//                CreatedAt = DateTime.UtcNow
//            };

//            await _context.ComplaintEmbeddings.AddAsync(complaintEmbedding);

//            await _context.SaveChangesAsync();
//        }
//    }
//}
