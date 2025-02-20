using AdilBooks.Interfaces;
using AdilBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AdilBooks.Controllers
{
    [Route("Authors")]
    
    public class AuthorPageController : Controller
    {
        private readonly IAuthorService _authorService;

        // Constructor with dependency injection for AuthorService
        public AuthorPageController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: Authors/List
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            IEnumerable<AuthorListDto> authors = await _authorService.ListAuthors();
            return View(authors);  // Looks for Views/Authors/List.cshtml
        }
        [HttpGet("Find/{id}")]
        public async Task<IActionResult> Find(int id)
        {
            var author = await _authorService.FindAuthor(id); // Fetch the author with books
            if (author == null)
            {
                return View("Error", new ErrorViewModel { Errors = new List<string> { "Author not found." } });
            }

            return View(author); // Pass an AuthorDto object, not a list
        }


        // POST: Authors/Add
        [Authorize]
        [HttpPost("Add")]
        public async Task<IActionResult> Add(AuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Add", authorDto);  // Stay on Add page with errors if input is invalid
            }

            ServiceResponse response = await _authorService.AddAuthor(authorDto);
            if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return View("Error", new ErrorViewModel { Errors = response.Messages });
            }
            TempData["SuccessMessage"] = "Author added successfully!";
            return RedirectToAction("Find", new { id = response.CreatedId });
        }

        // GET: Authors/Add
        [HttpGet("Add")]
        public IActionResult Add()
        {
            return View(); // Renders the "Add" form (Views/Authors/Add.cshtml)
        }

        // GET: Authors/Edit/{id}
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            AuthorDto author = await _authorService.FindAuthor(id);
            if (author == null)
            {
                return View("Error", new ErrorViewModel { Errors = new List<string> { "Author not found." } });
            }
            return View(author); // Looks for Views/Authors/Edit.cshtml
        }

        // POST: Authors/Update
        [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(AuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", authorDto);
            }

            ServiceResponse response = await _authorService.UpdateAuthor(authorDto);
            if (response.Status == ServiceResponse.ServiceStatus.Error || response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return View("Error", new ErrorViewModel { Errors = response.Messages });
            }
            TempData["SuccessMessage"] = "Author updated successfully!";
            return RedirectToAction("Find", new { id = authorDto.AuthorId });
        }

        // GET: Authors/ConfirmDelete/{id}
        [HttpGet("ConfirmDelete/{id}")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            AuthorDto author = await _authorService.FindAuthor(id);
            if (author == null)
            {
                return View("Error", new ErrorViewModel { Errors = new List<string> { "Author not found." } });
            }
            return View(author); // Looks for Views/Authors/ConfirmDelete.cshtml
        }

        // POST: Authors/Delete/{id}
        [Authorize]
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse response = await _authorService.DeleteAuthor(id);
            if (response.Status == ServiceResponse.ServiceStatus.Error || response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return View("Error", new ErrorViewModel { Errors = response.Messages });
            }
            TempData["SuccessMessage"] = "Author deleted successfully!";
            return RedirectToAction("List");
        }
    }
}
