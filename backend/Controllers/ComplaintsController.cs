//using backend.Dto;
//using backend.Models;
//using backend.Services;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ComplaintsController : ControllerBase
//    {
//        private readonly IDuplicateDetectionService _duplicateService;
//        [HttpPost("check-duplicates")]
//        public async Task<IActionResult> CheckDuplicates(
//    [FromBody] CreateComplaintDto request)
//        {
//            var complaint = new Complaints
//            {
//                Title = request.Title,
//                Description = request.Description,
//                Category = request.Category,
//                Location = request.Location
//            };

//            var matches =
//                await _duplicateService.FindDuplicatesAsync(
//                    complaint);

//            return Ok(new
//            {
//                HasDuplicates = matches.Any(),
//                Matches = matches
//            });
//        }
//    }
//}



//using backend.Dto;
//using backend.Models;
//using backend.Repositories;
//using backend.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace backend.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ComplaintsController : ControllerBase
//    {
//        private readonly IDuplicateDetectionService _duplicateService;
//        private readonly IComplaintsRepository _complaintsRepository;

//        public ComplaintsController(
//            IDuplicateDetectionService duplicateService)
//        {
//            _duplicateService = duplicateService;
//        }

//        [HttpPost("check-duplicates")]
//        public async Task<IActionResult> CheckDuplicates(
//            [FromBody] CreateComplaintDto request)
//        {
//            if (request == null)
//                return BadRequest("Invalid complaint data.");

//            var complaint = new Complaints
//            {
//                Title = request.Title,
//                Description = request.Description,
//                Category = request.Category,
//                Location = request.Location
//            };

//            var matches = await _duplicateService
//                .FindDuplicatesAsync(complaint);

//            return Ok(new
//            {
//                HasDuplicates = matches.Any(),
//                Matches = matches
//            });
//        }

//        [HttpPost("Addcomplaint")]
//        public IActionResult AddAffectedPeople([FromBody] CreateComplaintDto complaint)
//        {
//            var result = _complaintsRepository.AddComplaint(complaint);
//            return Ok(result);
//        }
//    }
//}



using backend.Dto;
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IDuplicateDetectionService _duplicateService;
        private readonly IComplaintsRepository _complaintsRepository;
        private readonly IAIEmbeddingService _aiEmbeddingService;

        // Constructor: Inject both dependencies
        public ComplaintsController(
            IDuplicateDetectionService duplicateService,
            IComplaintsRepository complaintsRepository,
            IAIEmbeddingService aiEmbeddingService,
            IDuplicateDetectionService duplicateDetectionService)
        {
            _duplicateService = duplicateService;
            _complaintsRepository = complaintsRepository;
            _aiEmbeddingService = aiEmbeddingService;
        }

        // Check duplicate complaints
        [HttpPost("check-duplicates")]
        public async Task<IActionResult> CheckDuplicates(
            [FromBody] CreateComplaintDto request)
        {
            if (request == null)
                return BadRequest("Invalid complaint data.");

            var complaint = new Complaints
            {
                Title = request.Title,
                Description = request.Description,
                Category = request.Category,
                Location = request.Location
            };

            var matches = await _duplicateService
                .FindDuplicatesAsync(complaint);

            return Ok(new
            {
                HasDuplicates = matches.Any(),
                Matches = matches
            });
        }

        // Add complaint
        //[HttpPost("Addcomplaint")]
        //public async Task<IActionResult> AddComplaint(
        //    [FromBody] CreateComplaintDto complaint)
        //{
        //    //if (complaint == null)
        //    //    return BadRequest("Invalid complaint data.");

        //    //var result = _complaintsRepository.AddComplaint(complaint);
        //    //float[] embedding = await _aiEmbeddingService.GenerateEmbeddingAsync(complaint.Description); // Call the embedding generation method


        //    //return Ok(result);

        //    if (complaint == null)
        //        return BadRequest("Invalid complaint data.");

        //    // 1. Save complaint
        //    var result = _complaintsRepository.AddComplaint(complaint);

        //    // 2. Generate embedding
        //    float[] embedding =
        //        await _aiEmbeddingService.GenerateEmbeddingAsync(
        //            complaint.Description
        //        );

        //    // 3. Save embedding into SQL Server
        //    await _complaintsRepository.SaveEmbeddingAsync(
        //        result.Id,
        //        embedding
        //    );

        //    return Ok(new
        //    {
        //        Message = "Complaint and embedding saved successfully",
        //        Complaint = result,
        //        EmbeddingDimensions = embedding.Length
        //    });
        //}

        //    [HttpPost("Addcomplaint")]
        //    public async Task<IActionResult> AddComplaint(
        //[FromBody] CreateComplaintDto complaint)
        //    {
        //        if (complaint == null)
        //            return BadRequest("Invalid complaint data.");

        //        // 1. Save complaint
        //        var result = _complaintsRepository.AddComplaint(complaint);

        //        // 2. Generate embedding
        //        float[] embedding =
        //            await _aiEmbeddingService.GenerateEmbeddingAsync(
        //                        complaint.Description,
        //                complaint.Location
        //            );

        //        // 3. Save embedding
        //        await _complaintsRepository.SaveEmbeddingAsync(
        //            result.Id,
        //            embedding
        //        );

        //        // 4. Return simple response without navigation properties
        //        return Ok(new
        //        {
        //            Message = "Complaint and embedding saved successfully",
        //            ComplaintId = result.Id,
        //            Title = result.Title,
        //            Description = result.Description,
        //            EmbeddingDimensions = embedding.Length
        //        });
        //    }


        [HttpPost("Addcomplaint")]
        public async Task<IActionResult> AddComplaint(
            [FromBody] CreateComplaintDto complaint)
        {
            if (complaint == null)
                return BadRequest("Invalid complaint data.");

            // 1. Create a temporary complaint object for duplicate checking
            var complaintToCheck = new Complaints
            {
                Title = complaint.Title,
                Description = complaint.Description,
                Category = complaint.Category,
                Location = complaint.Location
            };

            // 2. Check existing complaints BEFORE saving
            var duplicates =
                await _duplicateService.FindDuplicatesAsync(
                    complaintToCheck
                );

            // 3. Stop if duplicate complaints are found
            if (duplicates.Any())
            {
                return Conflict(new
                {
                    Message = "Similar complaints already exist. Complaint was not created.",
                    IsDuplicate = true,
                    SimilarComplaints = duplicates
                });
            }

            // 4. Save complaint
            var result = _complaintsRepository.AddComplaint(complaint);

            // 5. Generate embedding
            float[] embedding =
                await _aiEmbeddingService.GenerateEmbeddingAsync(
                    complaint.Description,
                    complaint.Location
                );

            // 6. Save embedding in SQL Server
            await _complaintsRepository.SaveEmbeddingAsync(
                result.Id,
                embedding
            );

            // 7. Return success response
            return Ok(new
            {
                Message = "Complaint and embedding saved successfully",
                ComplaintId = result.Id,
                Title = result.Title,
                Description = result.Description,
                Location = result.Location,
                EmbeddingDimensions = embedding.Length,
                IsDuplicate = false
            });
        }

    }
}
