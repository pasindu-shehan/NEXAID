using backend.Dto;
using backend.Models;

namespace backend.Repositories
{
    public interface IEmergencyCoordinatorRepository
    {
        List<EmergencyCoordinator> GetAllEmergencyCoordinators();
        string AddEmergencyCoordinator(EmergencyCoordinatorDto emergencyCoordinator);
        string UpdateEmergencyCoordinator(int id, EmergencyCoordinatorDto emergencyCoordinator);
        string DeleteEmergencyCoordinator(int id);
    }
}
