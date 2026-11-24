using backend.Dto;
using backend.Models;

namespace backend.Repositories
{
    public interface IComplaintsRepository
    {
        List<Complaints> GetAllComplaints();
        Complaints AddComplaint(CreateComplaintDto complaint);

        string UpdateComplaint(int id, CreateComplaintDto complaint);
        string DeleteComplaint(int id);

        Task SaveEmbeddingAsync(
    int complaintId,
    float[] embedding
);
    }
}
