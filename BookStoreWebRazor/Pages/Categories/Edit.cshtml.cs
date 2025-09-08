using BookStoreWebRazor.Data;
using BookStoreWebRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreWebRazor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        //[BindProperty]
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _context.Categories.FirstOrDefault(x => x.Id == id);
            }
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return NotFound(); 
            }
            var entity = _context.Categories.FirstOrDefault(x => x.Id == Category.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = Category.Name;
            entity.DisplayOrder = Category.DisplayOrder;

            _context.Categories.Update(entity);
            _context.SaveChanges();
         
            return RedirectToPage("Index");
        }
    }
}
