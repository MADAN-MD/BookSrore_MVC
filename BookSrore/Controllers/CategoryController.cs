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

                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }

        public IActionResult Edit(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var entity = _context.Categories.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }
                return View(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var entity = _context.Categories.FirstOrDefault(x => x.Id == model.Id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.Name = model.Name;
                entity.DisplayOrder = model.DisplayOrder;

                _context.Categories.Update(entity);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var entity = _context.Categories.FirstOrDefault(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(entity);
                _context.SaveChanges();
                TempData["success"] = "Category removed successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
