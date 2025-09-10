using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            try
            {
                if (model.Title == model.Description.ToString())
                {
                    ModelState.AddModelError("title", "Title and Description should be different.");
                }
                if (!ModelState.IsValid)
                {
                    return View();
                }

                _unitOfWork.Product.Add(model);
                _unitOfWork.Save();

                TempData["success"] = "Product created successfully.";
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
                var entity = _unitOfWork.Product.Get(x => x.Id == id);

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
        public IActionResult Edit(Product model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var entity = _unitOfWork.Product.Get(x => x.Id == model.Id);
                if (entity == null)
                {
                    return NotFound();
                }

                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.ISBN = model.ISBN;
                entity.Price = model.Price;
                entity.ListPrice = model.ListPrice;
                entity.Price50 = model.Price50;

                _unitOfWork.Product.Update(entity);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully.";
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
                var entity = _unitOfWork.Product.Get(x => x.Id == id);

                if (entity == null)
                {
                    return NotFound();
                }

                _unitOfWork.Product.Remove(entity);
                _unitOfWork.Save();
                TempData["success"] = "Product removed successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
