using Library.Application.DTOs;
using Library.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService service,ILogger<BooksController> logger)
        {
            _service = service;
            _logger = logger;
              
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
        {
            var createdBook = await _service.AddBookAsync(createBookDto);
            return Ok(createdBook);
        
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _service.GetAllBooksAsync();
            return Ok(books);
        
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var book = await _service.GetBookByIdAsync(id);
            if (book is null)
                return NotFound(new { error = "Not Found" });
            return Ok(book);
        
        }
        [HttpGet("availableBooks")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            var books = await _service.GetAvailableBooksAsync();
            return Ok(books);
        
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto updateBookDto)
        {

            var updated = await _service.UpdateBookAsync(id, updateBookDto);
            return Ok(updated);
        
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveBookAsync(id);
            return NoContent();
        }





    }
}
