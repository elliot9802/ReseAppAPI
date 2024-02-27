using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppReseApplikationenWApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommentsController : Controller
    {
        private readonly IReseAppService _service;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(IReseAppService service, ILogger<CommentsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> ReadComments(
            [FromQuery] bool seeded = true,
            [FromQuery] bool flat = true,
            [FromQuery] string filter = null,
            [FromQuery] int pageNr = 0,
            [FromQuery] int pageSize = 1000)
        {
            try
            {
                var items = await _service.ReadCommentsAsync(seeded, flat, filter?.Trim()?.ToLower(), pageNr, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to read comments with filter: {Filter}", filter);
                return BadRequest($"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}