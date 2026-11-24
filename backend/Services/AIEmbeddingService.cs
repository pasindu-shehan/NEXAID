//using backend.Dto;

//namespace backend.Services
//{
//    public class AIEmbeddingService : IAIEmbeddingService
//    {
//        private readonly HttpClient _httpClient;

//        public AIEmbeddingService(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        public async Task<float[]> GenerateEmbeddingAsync(
//            string description)
//        {
//            var response = await _httpClient.PostAsJsonAsync(
//                "/generate-embedding",
//                new { description }
//            );

//            response.EnsureSuccessStatusCode();

//            var result = await response.Content
//                .ReadFromJsonAsync<EmbeddingResponseDto>();

//            if (result?.Embedding == null ||
//                result.Embedding.Length != 384)
//            {
//                throw new InvalidOperationException(
//                    "Invalid embedding returned by AI service.");
//            }

//            return result.Embedding;
//        }
//        }
//}


using backend.Dto;

namespace backend.Services
{
    public class AIEmbeddingService : IAIEmbeddingService
    {
        private readonly HttpClient _httpClient;

        public AIEmbeddingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<float[]> GenerateEmbeddingAsync(
            string description,
            string? location)
        {
            // Combine description and location
            string embeddingText = $"""
                Complaint Description: {description}
                Complaint Location: {location ?? "Unknown"}
                """;

            var response = await _httpClient.PostAsJsonAsync(
                "/generate-embedding",
                new { description = embeddingText }
            );

            response.EnsureSuccessStatusCode();

            var result = await response.Content
                .ReadFromJsonAsync<EmbeddingResponseDto>();

            if (result?.Embedding == null ||
                result.Embedding.Length != 384)
            {
                throw new InvalidOperationException(
                    "Invalid embedding returned by AI service.");
            }

            return result.Embedding;
        }
    }
}
