using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopTuto.Data;
using OnlineBookShopTuto.Models;

namespace OnlineBookShopTuto.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            var query = _context.Books.Include(b => b.Category).AsQueryable();
            if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }
            var books = await query.OrderByDescending(b => b.Id).ToListAsync();

            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            return View(books);
        }
    }
}