using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppReseApplikationenWApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GuestController : Controller
    {
        private readonly IReseAppService _service;
        private readonly ILogger<GuestController> _logger;

        public GuestController(IReseAppService service, ILogger<GuestController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            try
            {
                var info = await _service.InfoAsync;
                return Ok(info);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error fetching guest information");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

