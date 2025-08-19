using Microsoft.AspNetCore.Mvc;
using Rebekah_As_A_Service.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Rebekah_As_A_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RebekahFactsController(ILogger<RebekahFactsController> logger) : ControllerBase
    {
        //add db accessor 
        //this should update

        private readonly ILogger<RebekahFactsController> _logger = logger;

        //freaking what

        public class RouteName
        {
            public const string GetRebekahFactsByCategory = nameof(GetRebekahFactsByCategory);
        }

        [HttpGet("api/rebekah/fact/{factCategory}")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public FactResponse GetRebekahFactsByCategoryAsync([FromRoute][Required] string factCategory)
        {
            return new FactResponse
            {
                Category = FactCategoryEnum.Fun.ToString(),
                Fact = "randomstring for now"
            };
        }
    }
}
