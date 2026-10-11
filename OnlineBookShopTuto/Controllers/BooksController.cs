using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopTuto.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBookShopTuto.Models;

namespace OnlineBookShopTuto.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.Include(b => b.Category).ToListAsync();
            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if(ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        //GET: /Books/Edit/2 => hiện form có sẵn dữ liệu cũ
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        //POST: /Books/Edit/2 => nhận dữ liệu mới và cập nhật
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if(id != book.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }


        //GET: /Books/Delete/2 => hiện trang xác nhận
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if(book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        //POST: /Books/Delete/2 => Xoá thật
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
