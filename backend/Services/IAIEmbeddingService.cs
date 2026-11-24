namespace backend.Services
{
    public interface IAIEmbeddingService
    {
        Task<float[]> GenerateEmbeddingAsync(string description,
        string? location);
    }
}
