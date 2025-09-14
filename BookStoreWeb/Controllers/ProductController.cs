using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll().ToList();

            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                });

            //ViewBag.CategoryList = categoryList;
            ProductViewModel viewModel = new ProductViewModel()
            {
                CategoryList = categoryList,
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                return View(viewModel);
            }
            else
            {
                //update
                viewModel.Product = _unitOfWork.Product.Get(x => x.Id == id);
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel model, IFormFile? file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    IEnumerable<SelectListItem> categoryList = _unitOfWork.Category
                                                                .GetAll()
                                                                .Select(c => new SelectListItem
                                                                {
                                                                    Text = c.Name,
                                                                    Value = c.Id.ToString(),
                                                                });
                    ProductViewModel viewModel = new ProductViewModel()
                    {
                        CategoryList = categoryList,
                        Product = model.Product
                    };
                    return View(viewModel);
                }

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    model.Product.ImageUrl = @"\images\products\" + fileName;
                }

                _unitOfWork.Product.Add(model.Product);
                _unitOfWork.Save();

                TempData["success"] = "Product created successfully.";
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
