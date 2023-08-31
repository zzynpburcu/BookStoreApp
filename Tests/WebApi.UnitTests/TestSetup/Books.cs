using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                    new Book { Title = "Lean Startup", GenreId = 1, PageCount = 200, AuthorId = 2, PublishDate = new DateTime(2001, 06, 12) },
                    new Book { Title = "Herland", GenreId = 2, PageCount = 250, AuthorId = 3, PublishDate = new DateTime(2010, 05, 23) },
                    new Book { Title = "Dune", GenreId = 1, PageCount = 540, AuthorId = 1, PublishDate = new DateTime(2001, 12, 21) });
        }
    }
}