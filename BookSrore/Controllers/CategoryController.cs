using BookSrore.Data;
using BookSrore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookSrore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categoryList = _context.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            try
            {
                if (model.Name == model.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name", "Name and order value should be different.");
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }

                _context.Categories.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }
    }
}
