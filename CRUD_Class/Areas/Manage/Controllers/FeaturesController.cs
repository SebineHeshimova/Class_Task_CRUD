using CRUD_Class.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace CRUD_Class.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FeaturesController : Controller
    {
        public readonly AppDbContext _context;
        public FeaturesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Features> features = _context.Features.ToList();
            return View(features);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Features features)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            _context.Features.Add(features);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Features features = _context.Features.FirstOrDefault(x => x.Id == id);
            if(features == null) return NotFound();          
            return View(features);
        }
        [HttpPost]
        public IActionResult Update(Features features)
        {
            Features existFeatures=_context.Features.FirstOrDefault(x => x.Id == features.Id);
            if(existFeatures == null) return NotFound();
            existFeatures.Title = features.Title;
            existFeatures.Description = features.Description;
            existFeatures.Logo = features.Logo;
           
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Features features = _context.Features.FirstOrDefault(x => x.Id == id);
            if(features==null) return NotFound();

            return View(features);
        }
        [HttpPost]
        public IActionResult Delete(Features features)
        {
            Features existFeatures=_context.Features.FirstOrDefault(x => x.Id == features.Id);
            if (existFeatures == null) return NotFound();
            _context.Remove(existFeatures);
            return RedirectToAction("index");
        }
    }
}
