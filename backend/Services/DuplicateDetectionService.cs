using backend.Dto;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace backend.Services
{
    public class DuplicateDetectionService : IDuplicateDetectionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAIEmbeddingService _aiService;

        public DuplicateDetectionService(
            ApplicationDbContext context,
            IAIEmbeddingService aiService)
        {
            _context = context;
            _aiService = aiService;
        }

        //public async Task<List<DuplicateComplaintDto>>
        //    FindDuplicatesAsync(Complaints complaint)
        //{
        //    // Generate embedding for new complaint
        //    var newEmbedding =
        //        await _aiService.GenerateEmbeddingAsync(
        //            complaint.Description,
        //            complaint.Location);

        //    // Retrieve existing complaints and embeddings
        //    var existing = await (
        //        from c in _context.Complaints
        //        join e in _context.ComplaintEmbeddings
        //            on c.Id equals e.ComplaintId
        //        where c.Id != complaint.Id
        //        select new
        //        {
        //            Complaint = c,
        //            e.EmbeddingJson
        //        }
        //    ).ToListAsync();

        //    var matches = new List<DuplicateComplaintDto>();

        //    foreach (var item in existing)
        //    {
        //        // Ignore clearly unrelated categories
        //        if (!string.Equals(
        //            complaint.Category,
        //            item.Complaint.Category,
        //            StringComparison.OrdinalIgnoreCase))
        //        {
        //            continue;
        //        }

        //        // Compare geographical areas
        //        if (!string.Equals(
        //            complaint.Location,
        //            item.Complaint.Location,
        //            StringComparison.OrdinalIgnoreCase))
        //        {
        //            continue;
        //        }

        //        var oldEmbedding =
        //            JsonSerializer.Deserialize<float[]>(
        //                item.EmbeddingJson);

        //        if (oldEmbedding == null ||
        //            oldEmbedding.Length != newEmbedding.Length)
        //            continue;

        //        double similarity =
        //            SimilarityCalculator.CosineSimilarity(
        //                newEmbedding,
        //                oldEmbedding);

        //        // Initial candidate threshold
        //        if (similarity >= 0.70)
        //        {
        //            matches.Add(new DuplicateComplaintDto
        //            {
        //                ComplaintId = item.Complaint.Id,
        //                Title = item.Complaint.Title,
        //                Description = item.Complaint.Description,
        //                Category = item.Complaint.Category,
        //                Location = item.Complaint.Location,
        //                SimilarityScore = Math.Round(
        //                    similarity * 100, 2)
        //            });
        //        }
        //    }

        //    return matches
        //        .OrderByDescending(x => x.SimilarityScore)
        //        .Take(10)
        //        .ToList();
        //}


        public async Task<List<DuplicateComplaintDto>>
            FindDuplicatesAsync(Complaints complaint)
        {
            // 1. Generate embedding for new complaint
            var newEmbedding =
                await _aiService.GenerateEmbeddingAsync(
                    complaint.Description,
                    complaint.Location
                );

            // 2. Retrieve only complaints with matching category and location
            var existing = await (
                from c in _context.Complaints.AsNoTracking()
                join e in _context.ComplaintEmbeddings.AsNoTracking()
                    on c.Id equals e.ComplaintId
                where c.Id != complaint.Id
                      && c.Category == complaint.Category
                      && c.Location == complaint.Location
                select new
                {
                    ComplaintId = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    Category = c.Category,
                    Location = c.Location,
                    EmbeddingJson = e.EmbeddingJson
                }
            ).ToListAsync();

            var matches = new List<DuplicateComplaintDto>();

            // 3. Compare embeddings
            foreach (var item in existing)
            {
                var oldEmbedding =
                    JsonSerializer.Deserialize<float[]>(
                        item.EmbeddingJson
                    );

                if (oldEmbedding == null ||
                    oldEmbedding.Length != newEmbedding.Length)
                    continue;

                double similarity =
                    SimilarityCalculator.CosineSimilarity(
                        newEmbedding,
                        oldEmbedding
                    );

                // 4. Similarity threshold
                if (similarity >= 0.70)
                {
                    matches.Add(new DuplicateComplaintDto
                    {
                        ComplaintId = item.ComplaintId,
                        Title = item.Title,
                        Description = item.Description,
                        Category = item.Category,
                        Location = item.Location,
                        SimilarityScore = Math.Round(
                            similarity * 100, 2)
                    });
                }
            }

            // 5. Return top 10 matches
            return matches
                .OrderByDescending(x => x.SimilarityScore)
                .Take(10)
                .ToList();
        }

    }
}
