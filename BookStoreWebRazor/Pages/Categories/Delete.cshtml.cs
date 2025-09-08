using BookStoreWebRazor.Data;
using BookStoreWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreWebRazor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        //[BindProperty]
        public Category Category { get; set; }
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int? id) 
        {
            if (id != null && id != 0)
            {
                Category = _context.Categories.Find(Category.Id == id);
            }

            _context.Categories.Remove(Category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
