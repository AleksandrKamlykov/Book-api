using Book_api.Data;
using Book_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Book_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost("{userId}/cart")]
        public async Task<IActionResult> AddToCart(int userId,  int bookId)
        {
            var user = await _context.Users.Include(u => u.Carts).ThenInclude(c => c.Items).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(bookId);
            if (book == null) {
                return NotFound();
            }

            var cart = user.Carts.FirstOrDefault() ?? new Cart { Items = new List<Book>() };
            cart.Items.Add(book);

            if (user.Carts.Count == 0)
            {
                user.Carts.Add(cart);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{userId}/cart")]
        public async Task<ActionResult<Cart>> GetCart(int userId)
        {
            var user = await _context.Users.Include(u => u.Carts).ThenInclude(c => c.Items).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Carts.Count == 0)
            {
                return NotFound();
            }

            return user.Carts.First();
        }

        [HttpPost("{userId}/order")]
        public async Task<IActionResult> PlaceOrder(int userId)
        {
            var user = await _context.Users.Include(u => u.Carts).ThenInclude(c => c.Items).Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Carts.Count == 0)
            {
                return NotFound();
            }

            var cart = user.Carts.First();
            var order = new Order
            {
                UserId = userId,
                Books = cart.Items,
                OrderDate = DateTime.Now
            };

            user.Orders.Add(order);
            user.Carts.Remove(cart);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{userId}/orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderHistory(int userId)
        {
            var user = await _context.Users.Include(u => u.Orders).ThenInclude(o => o.Books).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return user.Orders;
        }
    }
}
