namespace backend.Repositories
{
    public interface IComplaintEmbeddingRepository
    {
        Task SaveEmbeddingAsync(
    int complaintId,
    float[] embedding
);
    }
}
