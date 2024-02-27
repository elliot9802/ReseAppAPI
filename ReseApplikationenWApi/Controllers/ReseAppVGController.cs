using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services;

namespace AppReseApplikationenWApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class aReseAppVGController : Controller
    {
        private readonly IReseAppService _service;
        private readonly ILogger<aReseAppVGController> _logger;

        public aReseAppVGController(IReseAppService service, ILogger<aReseAppVGController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Create Operations

        [HttpPost()]
        public async Task<IActionResult> CreatePerson([FromBody] csPersonCdto personCreateDto)
        {
            try
            {
                var _src = new csPersonCUdto
                {
                    FirstName = personCreateDto.FirstName,
                    LastName = personCreateDto.LastName
                };

                var createdPerson = await _service.CreatePersonAsync(_src);
                return Ok(createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating person.");
                return BadRequest($"Could not create person. Error {ex.Message}");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAttraction([FromBody] csAttractionCdto attractionCreateDto)
        {
            try
            {
                var _src = new csAttractionCUdto
                {
                    AttractionTitle = attractionCreateDto.AttractionTitle,
                    AttractionDescription = attractionCreateDto.AttractionDescription,
                    Category = attractionCreateDto.Category
                };

                var createdAttraction = await _service.CreateAttractionAsync(_src);
                return Ok(createdAttraction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating attraction.");
                return BadRequest($"Could not create. Error {ex.Message}");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> AddCommentToAttraction([FromBody] CommentAttractionRequest request)
        {
            if (request == null || request.AttractionId == Guid.Empty || request.PersonId == Guid.Empty || string.IsNullOrWhiteSpace(request.Comment))
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                await _service.AddCommentToAttraction(request.AttractionId, request.PersonId, request.Comment);
                return Ok($"Comment: '{request.Comment}' added to attraction with id: '{request.AttractionId}' by User with id: '{request.PersonId}'");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding comment to attraction.");
                return BadRequest("Could not add comment to attraction.");
            }
        }

        #endregion Create Operations

        #region Update Operations

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttraction(Guid id, [FromBody] csAttractionCUdto _src)
        {
            if (id != _src.AttractionId)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                var updatedAttraction = await _service.UpdateAttractionAsync(_src);
                return Ok(updatedAttraction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating attraction.");
                return BadRequest($"Could not update. Error {ex.Message}");
            }
        }

        #endregion Update Operations

        #region Delete Operations

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttraction(Guid id)
        {
            try
            {
                var deletedAttraction = await _service.DeleteAttractionAsync(id);
                return Ok(deletedAttraction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting attraction.");
                if (ex is ArgumentException) return NotFound(ex.Message);
                return StatusCode(500, "An error occurred while deleting the attraction.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            try
            {
                var deletedPerson = await _service.DeletePersonAsync(id);
                return Ok(deletedPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting attraction.");
                if (ex is ArgumentException) return NotFound(ex.Message);
                return StatusCode(500, "An error occurred while deleting the attraction.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttractionComment(Guid id)
        {
            try
            {
                var deleteAttractionCmnt = await _service.DeleteCommentAsync(id);
                return Ok(deleteAttractionCmnt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting attraction.");
                if (ex is ArgumentException) return NotFound(ex.Message);
                return StatusCode(500, "An error occurred while deleting the attraction.");
            }
        }

        #endregion Delete Operations
    }
}