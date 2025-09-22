using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rebekah_As_A_Service.Models;
using Rebekah_As_A_Service.Processors.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Rebekah_As_A_Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminProcessor _processor;

        public AdminController(ILoggerFactory loggerFactory, IAdminProcessor processor)
        {
            _logger = loggerFactory.CreateLogger<AdminController>();
            _processor = processor;
        }

        [HttpPut("api/admin/fact/update/{factID}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> UpdateRebekahFactsAsync([FromRoute][Required] int factID, [FromBody] FactUpdateRequest request)
        {
            if (factID <= 0)
            {
                return BadRequest("FactId must be greater than 0");
            }
            if (request == null)
            {
                return BadRequest("Request cannot be null");
            }
            if (request.Category == null || request.Category == "" || request.Description == null || request.Description == "")
            {
                return BadRequest("Request must include non-empty Category and Description for update");
            }
            try
            {
                var resp =  await _processor.UpdateFactByID(factID, request);
                if(resp == null)
                {
                    return NotFound($"Fact could not be updated, FactId {factID} could not be found");
                }
                return Ok(resp);
            }

            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to update fact. ErrorId {errorId}");
                var content = new ContentResult
                {
                    Content = factID.ToString(),
                    StatusCode = 500
                };
                return content;
            }
        }

        [HttpDelete("api/admin/fact/delete/{factCategory}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> DeleteRebekahFactsAsync([FromRoute][Required] int factID)
        {
            if (factID <= 0)
            {
                return BadRequest("FactId must be greater than 0");
            }
            try
            {
                var resp = await _processor.DeleteFactByIDAsync(factID);
                if (resp == null)
                {
                    return NotFound($"Fact could not be deleted, FactId {factID} could not be found");
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to update fact. ErrorId {errorId}");
                var content = new ContentResult
                {
                    Content = factID.ToString(),
                    StatusCode = 500
                };
                return content;
            }
        }

        [HttpPost("api/admin/fact/create")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> CreateRebekahFactsAsync([FromBody][Required] FactCreateRequest createRequest)
        {
            if (createRequest == null)
            {
                return BadRequest("Invalid request, cannot be null");
            }
            if (createRequest.CategoryID <= 0)
            {
                return BadRequest("CategoryID must be greater than 0");
            }
            if (createRequest.FactDescription == null || createRequest.FactDescription == "")
            {
                return BadRequest("FactDescription is required");
            }
            try
            {
                var result = await _processor.CreateNewFactAsync(createRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to insert fact. ErrorId {errorId}");
                var content = new ContentResult
                {
                    //do this better
                    Content = createRequest.ToString(),
                    StatusCode = 500
                };
                return content;
            }
        }

        [HttpPost("api/admin/category/create/{factCategory}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> CreateFactCategoryAsync([FromRoute][Required] string factCategory)
        {
            try
            {
                //is this safe? 
                //need to see how best way to do
                await _processor.AddFactCategoryAsync(factCategory);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to create new Fact Category. ErrorId {errorId}");
                var content = new ContentResult
                {
                    Content = factCategory,
                    StatusCode = 500
                };
                return content;
            }
        }
    }
}
