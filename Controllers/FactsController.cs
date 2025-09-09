using Microsoft.AspNetCore.Mvc;
using Rebekah_As_A_Service.Processors;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Rebekah_As_A_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FactsController(ILogger<FactsController> logger) : ControllerBase
    { 
        private readonly ILogger<FactsController> _logger = logger;
        private readonly FactsProcessor _factsprocessor = new();


        public class RouteName
        {
            public const string GetRebekahFactsByCategory = nameof(GetRebekahFactsByCategory);
        }

        [HttpGet("api/rebekah/fact/{factCategory}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> GetRebekahFactsByCategoryAsync([FromRoute][Required] string factCategory)
        {
            if (factCategory == null || factCategory == "")
            {
                return BadRequest("Invalid Request. Category cannot be null");
            }
            try
            {
                var resp = await _factsprocessor.GetFactsByCategory(factCategory);
                if (resp.Count == 0)
                {
                    return NotFound("No Facts in that Category were found");
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to get fact. ErrorId {errorId}");
                var content = new ContentResult
                {
                    Content = factCategory,
                    StatusCode = 500
                };
                return content;
            }
        }

        [HttpGet("api/rebekah/categories")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> GetFactsCategoriesAsync()
        {
            try
            {
                var resp = await _factsprocessor.GetCategoriesAsync();
                if (resp.Count == 0)
                {
                    return NotFound("No Categories Found");
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to get fact. ErrorId {errorId}");
                var content = new ContentResult
                {
                    Content = ex.Message,
                    StatusCode = 500
                };
                return content;
            }
        }
    }
}
