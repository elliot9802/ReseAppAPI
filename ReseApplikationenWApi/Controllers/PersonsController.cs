using DbModels;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services;

namespace AppReseApplikationenWApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PersonsController : Controller
    {
        private readonly IReseAppService _service;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(IReseAppService service, ILogger<PersonsController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> ReadPersons(
            [FromQuery] bool seeded = true,
            [FromQuery] bool flat = true,
            [FromQuery] string filter = null,
            [FromQuery] int pageNr = 0,
            [FromQuery] int pageSize = 1000)
        {
            try
            {
                var items = await _service.ReadPersonsAsync(seeded, flat, filter?.Trim()?.ToLower(), pageNr, pageSize);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading persons");
                return BadRequest($"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadPerson(Guid id, [FromQuery] bool flat = false)
        {
            try
            {
                _logger.LogInformation($"Reading person by ID: {id}");

                var person = await _service.ReadPersonAsync(id, flat);
                if (person == null)
                {
                    return NotFound($"Item with id {id} cannot be found.");
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading person with id: {Id}", id);
                return BadRequest($"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadPersonDto(Guid id)
        {
            try
            {
                var person = await _service.ReadPersonAsync(id, false);
                if (person == null)
                {
                    return NotFound($"Item with id {id} cannot be found.");
                }

                var dto = new csPersonCUdto((csPersonDbM)person);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading person DTO with id: {Id}", id);
                return BadRequest($"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}