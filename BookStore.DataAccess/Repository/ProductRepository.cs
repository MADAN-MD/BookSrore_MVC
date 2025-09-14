using BookSrore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productInDb = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (productInDb != null)
            {
                productInDb.Title = product.Title;
                productInDb.ISBN = product.ISBN;
                productInDb.Price = product.Price;
                productInDb.Price50 = product.Price50;
                productInDb.ListPrice = product.ListPrice;
                productInDb.Price100 = product.Price100;
                productInDb.Description = product.Description;
                productInDb.CategoryId = product.CategoryId;
                productInDb.Author = product.Author;
                if (product.ImageUrl != null)
                {
                    productInDb.ImageUrl = product.ImageUrl;
                }
            }
            //_context.Products.Update(productInDb);
        }
    }
}
