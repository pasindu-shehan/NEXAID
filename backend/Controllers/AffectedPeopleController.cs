using backend.Dto;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AffectedPeopleController : ControllerBase
    {
        private readonly IAffextedPeopleRepository _affectedPeopleRepository;
        
        public AffectedPeopleController(IAffextedPeopleRepository affectedPeopleRepository)
        {
            _affectedPeopleRepository = affectedPeopleRepository;
        }

        [HttpGet("GetAllAffectedPeople")]
        public IActionResult GetAllAffectedPeople()
        {
            var affectedPeople = _affectedPeopleRepository.GetAllAffectedPeople();
            return Ok(affectedPeople);
        }

        [HttpPost("AddAffectedPeople")]
        public IActionResult AddAffectedPeople([FromBody] AffectedPeopleDto affectedPeople)
        {
            var result = _affectedPeopleRepository.AddAffectedPeople(affectedPeople);
            return Ok(result);
        }

        [HttpPut("UpdateAffectedPeople/{id}")]
        public IActionResult UpdateAffectedPeople(int id, [FromBody] AffectedPeopleDto affectedPeople)
        {
            var result = _affectedPeopleRepository.UpdateAffectedPeople(id, affectedPeople);
            return Ok(result);
        }

        [HttpDelete("DeleteAffectedPeople/{id}")]
        public IActionResult DeleteAffectedPeople(int id)
        {
            var result = _affectedPeopleRepository.DeleteAffectedPeople(id);
            return Ok(result);
        }
    }
}
