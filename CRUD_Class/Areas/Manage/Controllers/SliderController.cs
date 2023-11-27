using CRUD_Class.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Class.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        public readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Sliders> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Sliders Sliders)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _context.Sliders.Add(Sliders);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Sliders Sliders = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (Sliders == null) return NotFound();
            return View(Sliders);
        }
        [HttpPost]
        public IActionResult Update(Sliders Sliders)
        {
            Sliders existSliders = _context.Sliders.FirstOrDefault(x => x.Id == Sliders.Id);
            if (existSliders == null) return NotFound();
            existSliders.Title = Sliders.Title;
            existSliders.Description = Sliders.Description;
            existSliders.RedirectText=Sliders.RedirectText;
            existSliders.RedirectUrl=Sliders.RedirectUrl;
            existSliders.ImageUrl=Sliders.ImageUrl;

            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Sliders Sliders = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (Sliders == null) return NotFound();

            return View(Sliders);
        }
        [HttpPost]
        public IActionResult Delete(Sliders Sliders)
        {
            Sliders existSliders = _context.Sliders.FirstOrDefault(x => x.Id == Sliders.Id);
            if (existSliders == null) return NotFound();
            _context.Remove(existSliders);
            return RedirectToAction("index");
        }
    }
}
