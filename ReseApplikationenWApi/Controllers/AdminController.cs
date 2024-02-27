using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppReseApplikationenWApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {

        private readonly IReseAppService _service;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IReseAppService service, ILogger<AdminController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Seed([FromQuery] int count)
        {
            try
            {
                var info = await _service.SeedAsync(count);
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during seeding with count: {Count}", count);
                return BadRequest($"An error occurred while processing your request: {ex.InnerException?.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> RemoveSeed([FromQuery] bool seeded = true)
        {
            try
            {
                var info = await _service.RemoveSeedAsync(seeded);
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during removing seed: {Seeded}", seeded);
                return BadRequest($"An error occurred while processing your request: {ex.InnerException?.Message}");
            }
        }
    }
}

