using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{

    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbcontext;
        public int ID {get;set;}
        public DeleteBookCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x => x.Id == ID);
            if (book is null)
                throw new InvalidOperationException("Kitap mevcut değil");

            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }
}