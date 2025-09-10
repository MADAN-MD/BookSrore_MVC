using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookSrore.DataAccess.Data
{
    public class ApplicationDbContext: DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Biography", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Atomic Habits: An Easy & Proven Way to Build Good Habits & Break Bad Ones",
                    Description = "Tiny Changes, Remarkable Results\r\nNo matter your goals, Atomic Habits offers a proven framework for improving--every day. James Clear, one of the world's leading experts on habit formation, reveals practical strategies that will teach you exactly how to form good habits, break bad ones, and master the tiny behaviors that lead to remarkable results.",
                    ISBN= "B07D23CFGR",
                    Author="James Clear",
                    ListPrice=10,
                    Price = 8.5,
                    Price50= 5.5,
                    Price100= 3.5,
                },
                new Product
                {
                    Id = 2,
                    Title = "AI Engineering: Building Applications with Foundation Models",
                    Description = "Recent breakthroughs in AI have not only increased demand for AI products, they've also lowered the barriers to entry for those who want to build AI products. The model-as-a-service approach has transformed AI from an esoteric discipline into a powerful development tool that anyone can use. Everyone, including those with minimal or no prior AI experience, can now leverage AI models to build applications.",
                    ISBN = "9781098166304",
                    Author = "Chip Huyen",
                    ListPrice = 50,
                    Price = 40.5,
                    Price50 = 30.5,
                    Price100 = 25.5,
                },
                new Product
                {
                    Id = 3,
                    Title = "The Love Hypothesis",
                    Description = "As a third-year Ph.D. candidate, Olive Smith doesn't believe in lasting romantic relationships--but her best friend does, and that's what got her into this situation. Convincing Anh that Olive is dating and well on her way to a happily ever after was always going to take more than hand-wavy Jedi mind tricks: Scientists require proof. So, like any self-respecting biologist, Olive panics and kisses the first man she sees.",
                    ISBN = "B08T6XN4FP",
                    Author = "Ali Hazelwood",
                    ListPrice = 30,
                    Price = 20.5,
                    Price50 = 16.5,
                    Price100 = 14.5,
                }

                );

        }
    }
}
