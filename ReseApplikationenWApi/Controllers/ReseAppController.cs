using Microsoft.AspNetCore.Mvc;
using Services;

namespace AppReseApplikationenWApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class aReseAppController : Controller
    {
        private readonly IReseAppService _service;
        private readonly ILogger<aReseAppController> _logger;

        public aReseAppController(IReseAppService service, ILogger<aReseAppController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> FilterAttractions(
            [FromQuery] bool seeded = true,
            [FromQuery] bool flat = false,
            [FromQuery] string filter = null,
            [FromQuery] int pageNr = 0,
            [FromQuery] int pageSize = 1000)
        {
            try
            {
                var items = await _service.FilterAttractions(seeded, flat, filter?.Trim()?.ToLower(), pageNr, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering attractions.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AttractionsWithoutComments()
        {
            try
            {
                var info = await _service.AttractionsWithoutComments;
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving attractions without comments.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AttractionDetails(Guid id)
        {
            try
            {
                var atn = await _service.AttractionDetails(id);
                if (atn == null)
                {
                    return NotFound($"Item with id {id} cannot be found.");
                }
                return Ok(atn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving details for attraction with id {id}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UsersAndComments()
        {
            try
            {
                var info = await _service.UsersAndComments;
                return Ok(info);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving users and comments.");
                return BadRequest(ex.Message);
            }
        }
    }
}