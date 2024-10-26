using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Book_api.Models;
using Book_api.Data;

namespace Book_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BooksController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(string? title, string? author, string? genre)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.ToLower().Equals(title.ToLower()));
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.ToLower().Equals(author.ToLower()));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.ToLower().Equals(genre.ToLower()));
            }

            return await query.ToListAsync();
        }
    }

    
}
