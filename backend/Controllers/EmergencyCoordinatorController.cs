using backend.Dto;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyCoordinatorController : ControllerBase
    {
        private readonly IEmergencyCoordinatorRepository _emergencyCoordinatorRepository;

        public EmergencyCoordinatorController(IEmergencyCoordinatorRepository emergencyCoordinatorRepository)
        {
            _emergencyCoordinatorRepository = emergencyCoordinatorRepository;
        }

        [HttpGet("GetAllEmergencyCoordinators")]
        public IActionResult GetAllEmergencyCoordinators()
        {
            var emergencyCoordinators = _emergencyCoordinatorRepository.GetAllEmergencyCoordinators();
            return Ok(emergencyCoordinators);
        }

        [HttpPost("AddEmergencyCoordinator")]
        public IActionResult AddEmergencyCoordinator([FromBody] EmergencyCoordinatorDto emergencyCoordinator)
        {
            var result = _emergencyCoordinatorRepository.AddEmergencyCoordinator(emergencyCoordinator);
            return Ok(result);
        }

        [HttpPut("UpdateEmergencyCoordinator/{id}")]
        public IActionResult UpdateEmergencyCoordinator(int id, [FromBody] EmergencyCoordinatorDto emergencyCoordinator)
        {
            var result = _emergencyCoordinatorRepository.UpdateEmergencyCoordinator(id, emergencyCoordinator);
            return Ok(result);
        }

        [HttpDelete("DeleteEmergencyCoordinator/{id}")]
        public IActionResult DeleteEmergencyCoordinator(int id)
        {
            var result = _emergencyCoordinatorRepository.DeleteEmergencyCoordinator(id);
            return Ok(result);
        }
    }
}
