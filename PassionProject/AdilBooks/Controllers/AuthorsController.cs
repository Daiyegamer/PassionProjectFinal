using AdilBooks.Models;
using AdilBooks.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AdilBooks.Interfaces;

namespace AdilBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        // Dependency Injection
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> ListAuthors()
        {
            var authors = await _authorService.ListAuthors();
            return Ok(new { message = "Authors retrieved successfully.", data = authors });
        }

        [HttpGet("Find/{id}")]
        public async Task<IActionResult> FindAuthor(int id)
        {
            var author = await _authorService.FindAuthor(id);
            if (author == null)
            {
                return NotFound(new { error = "NotFound", message = "Author not found." });
            }
            return Ok(new { message = "Author retrieved successfully.", data = author });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAuthor(AuthorDto authorDto)
        {
            var response = await _authorService.UpdateAuthor(authorDto);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(new { error = "NotFound", message = response.Messages });
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, new { error = "InternalServerError", message = response.Messages });
            }

            return NoContent();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAuthor(AuthorDto authorDto)
        {
            var response = await _authorService.AddAuthor(authorDto);
            if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, new { error = "InternalServerError", message = response.Messages });
            }
            return CreatedAtAction(nameof(FindAuthor), new { id = response.CreatedId }, new { message = "Author added successfully.", data = authorDto });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var response = await _authorService.DeleteAuthor(id);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(new { error = "NotFound", message = response.Messages });
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, new { error = "InternalServerError", message = response.Messages });
            }

            return NoContent();
        }
    }
}
