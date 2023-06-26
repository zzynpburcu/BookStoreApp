using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public int ID {get; set;}
        public GetBookByIdQuery(BookStoreDbContext _context)
        {
            _dbContext = _context;
        }

         public BooksViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == ID).SingleOrDefault();
            if (book is null)
               throw new InvalidOperationException("Kitap mevcut değil");
            BooksViewModel vm = new BooksViewModel(){
                    Title = book.Title,
                    Genre =((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = book.PageCount
                };
            return vm;
           
        }
    }
}