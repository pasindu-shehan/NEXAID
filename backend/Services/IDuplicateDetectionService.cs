using backend.Dto;
using backend.Models;

namespace backend.Services
{
    public interface IDuplicateDetectionService
    {
        Task<List<DuplicateComplaintDto>> FindDuplicatesAsync(
    Complaints complaint);
    }
}
