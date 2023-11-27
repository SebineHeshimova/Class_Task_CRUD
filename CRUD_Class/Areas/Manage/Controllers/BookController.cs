using CRUD_Class.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Class.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Book> book = _context.Books.ToList();
            return View(book);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Author.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags= _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            ViewBag.Authors = _context.Author.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            if (!ModelState.IsValid) return View(book);
            if (!_context.Author.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found!");
                return View();
            }
            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not found!");
                return View();
            }
            bool check = true;
            if (book.TagIds != null)
            {
                foreach(var tagId in book.TagIds)
                {
                    if(_context.Tags.Any(x=>x.Id == tagId))
                    {
                        check = false;
                        break;
                    }
                }            
            }
            if (!check)
            {
                foreach(var tagId in book.TagIds)
                {
                    BookTag bookTag = new BookTag()
                    {
                        Book = book,
                        TagId = tagId
                    };
                    _context.BookTags.Add(bookTag);
                }
            }
            else
            {
                ModelState.AddModelError("TagId", "Tag not faund!");
                return View();
            }
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Authors = _context.Author.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            Book book = _context.Books.Include(eb => eb.BookTags).FirstOrDefault(b => b.Id == id);
            book.TagIds=_context.BookTags.Where(bt=>bt.BookId == id).Select(x=>x.TagId).ToList();
            
            if (book == null) return NotFound();
            return View(book);
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            ViewBag.Authors = _context.Author.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (!ModelState.IsValid) return View(book);
            if (!_context.Author.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author not found!");
                return View();
            }
            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre not found!");
                return View();
            }
            Book existBook=_context.Books.Include(eb=>eb.BookTags).FirstOrDefault(b=>b.Id==book.Id);
            existBook.BookTags.RemoveAll(eb=>!book.TagIds.Any( ti=>eb.TagId==ti));
           foreach(var tagId in book.TagIds.Where(ti => !existBook.BookTags.Any(eb => eb.TagId == ti)))
            {
                BookTag bookTag = new BookTag()
                {
                    TagId = tagId
                };
                existBook.BookTags.Add(bookTag);

            }

            if (existBook == null) return NotFound();
            existBook.Name = book.Name;
            existBook.Description = book.Description;
            existBook.CostPrice = book.CostPrice;
            existBook.SalePrice = book.SalePrice;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.Code= book.Code;
            existBook.Genre= book.Genre;
            existBook.Author= book.Author;
            existBook.Tax= book.Tax;
            _context.Books.Add(existBook);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //public IActionResult Delete(int id)
        //{

        //}

    }
}
