using Microsoft.AspNetCore.Mvc;
using Rebekah_As_A_Service.Processors.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Rebekah_As_A_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FactsController : ControllerBase
    { 
        private readonly ILogger<FactsController> _logger;
        private readonly IFactsProcessor _factsProcessor;

        public FactsController(ILoggerFactory logFactory, IFactsProcessor factsProcessor)
        {
            _factsProcessor = factsProcessor;
            _logger = logFactory.CreateLogger<FactsController>();
        }
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
                var resp = await _factsProcessor.GetFactsByCategory(factCategory);
                if (resp.Count == 0)
                {
                    return NotFound("No Facts in the provided Category were found");
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
        public async Task<IActionResult> GetFactCategoriesAsync()
        {
            try
            {
                var resp = await _factsProcessor.GetCategoriesAsync();
                if (resp.Count == 0)
                {
                    return NotFound("No Categories Found");
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(new EventId(), ex, "Error while trying to get fact category. ErrorId {errorId}");
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
