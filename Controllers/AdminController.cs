using Microsoft.AspNetCore.Mvc;
using Rebekah_As_A_Service.Models;
using Rebekah_As_A_Service.Processors;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Rebekah_As_A_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController(ILogger<AdminController> logger) : ControllerBase
    {
        private readonly ILogger<AdminController> _logger = logger;
        private readonly AdminProcessor _processor = new();

        [HttpPut("api/admin/fact/update/{factID}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> UpdateRebekahFactsAsync([FromRoute][Required] int factID, [FromBody] FactUpdateRequest request)
        {
            try
            {
                var resp =  _processor.UpdateFactByID(factID, request);
                return Ok(resp);
            }

            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to get fact. ErrorId {errorId}");
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
            //placeholder for build
            return null;
        }

        [HttpPost("api/admin/fact/create")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> CreateRebekahFactsAsync([FromRoute][Required] string factCategory)
        {
            //placeholder for build
            return null;
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
