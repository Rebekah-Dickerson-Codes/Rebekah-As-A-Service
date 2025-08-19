using Microsoft.AspNetCore.Mvc;
using Rebekah_As_A_Service.Processors;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Rebekah_As_A_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebekahFactsController(ILogger<RebekahFactsController> logger) : ControllerBase
    { 
        private readonly ILogger<RebekahFactsController> _logger = logger;
        private readonly FactsProcessor _factsprocessor = new();


        public class RouteName
        {
            public const string GetRebekahFactsByCategory = nameof(GetRebekahFactsByCategory);
        }

        [HttpGet("api/rebekah/fact/{factCategory}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<ActionResult> GetRebekahFactsByCategoryAsync([FromRoute][Required] string factCategory)
        {
            if (factCategory == null)
            {
                return BadRequest("Invalid Request. Category cannot be null");
            }
            try
            {
                var resp = await _factsprocessor.GetFactByCategory(factCategory);
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
    }
}
