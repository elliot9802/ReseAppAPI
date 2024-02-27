using DbModels;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services;

namespace AppReseApplikationenWApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AttractionsController : Controller
    {
        private readonly IReseAppService _service;
        private readonly ILogger<AttractionsController> _logger;

        public AttractionsController(IReseAppService service, ILogger<AttractionsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> ReadAttractions(
            [FromQuery] bool seeded = true,
            [FromQuery] bool flat = true,
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
                _logger.LogError(ex, "Error reading attractions");
                return BadRequest($"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadAttraction(Guid id, [FromQuery] bool flat = false)
        {
            try
            {
                _logger.LogInformation($"Reading attraction by ID: {id}");

                var attraction = await _service.ReadAttractionAsync(id, flat);
                if (attraction == null)
                {
                    return NotFound($"Item with id {id} cannot be found");
                }

                return Ok(attraction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading person with id: {Id}", id);
                return BadRequest($"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadAttractionDto(Guid id)
        {
            try
            {
                _logger.LogInformation($"Reading attraction DTO by ID: {id}");

                var attraction = await _service.ReadAttractionAsync(id, false);
                if (attraction == null)
                {
                    return NotFound($"Item with id {id} cannot be found");
                }

                var dto = new csAttractionCUdto((csAttractionDbM)attraction);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading person with id: {Id}", id);
                return BadRequest($"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}