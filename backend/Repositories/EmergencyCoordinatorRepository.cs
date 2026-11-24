using backend.Dto;
using backend.Models;

namespace backend.Repositories
{
    public class EmergencyCoordinatorRepository : IEmergencyCoordinatorRepository
    {
        private readonly ApplicationDbContext _context;
        public EmergencyCoordinatorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EmergencyCoordinator> GetAllEmergencyCoordinators()
        {
            return _context.EmergencyCoordinators.ToList();
        }

        public string AddEmergencyCoordinator(EmergencyCoordinatorDto emergencyCoordinatorDto)
        {
            var emergencyCoordinator = new EmergencyCoordinator
            {
                Name = emergencyCoordinatorDto.Name,
                Age = emergencyCoordinatorDto.Age,
                Gender = emergencyCoordinatorDto.Gender,
                ContactNumber = emergencyCoordinatorDto.ContactNumber,
                Email = emergencyCoordinatorDto.Email,
                Address = emergencyCoordinatorDto.Address,
                Country = emergencyCoordinatorDto.Country,
                NeedsDescription = emergencyCoordinatorDto.NeedsDescription
            };
            _context.EmergencyCoordinators.Add(emergencyCoordinator);
            _context.SaveChanges();
            return "Emergency Coordinator Added Successfully";
        }

        public string UpdateEmergencyCoordinator(int id, EmergencyCoordinatorDto emergencyCoordinatorDto)
        {
            var emergencyCoordinator = _context.EmergencyCoordinators.FirstOrDefault(ec => ec.Id == id);
            if (emergencyCoordinator == null)
            {
                return "Emergency Coordinator not found";
            }
            emergencyCoordinator.Name = emergencyCoordinatorDto.Name;
            emergencyCoordinator.Age = emergencyCoordinatorDto.Age;
            emergencyCoordinator.Gender = emergencyCoordinatorDto.Gender;
            emergencyCoordinator.ContactNumber = emergencyCoordinatorDto.ContactNumber;
            emergencyCoordinator.Email = emergencyCoordinatorDto.Email;
            emergencyCoordinator.Address = emergencyCoordinatorDto.Address;
            emergencyCoordinator.Country = emergencyCoordinatorDto.Country;
            emergencyCoordinator.NeedsDescription = emergencyCoordinatorDto.NeedsDescription;
            _context.EmergencyCoordinators.Update(emergencyCoordinator);
            _context.SaveChanges();
            return "Emergency Coordinator Updated Successfully";
        }

        public string DeleteEmergencyCoordinator(int id)
        {
            var emergencyCoordinator = _context.EmergencyCoordinators.FirstOrDefault(ec => ec.Id == id);
            if (emergencyCoordinator == null)
            {
                return "Emergency Coordinator not found";
            }
            _context.EmergencyCoordinators.Remove(emergencyCoordinator);
            _context.SaveChanges();
            return "Emergency Coordinator Deleted Successfully";
        }
    }
}
