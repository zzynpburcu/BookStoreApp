using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{

    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbcontext;
        public int ID {get;set;}
        public DeleteAuthorCommand(BookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(x => x.Id == ID);
            var book = _dbcontext.Books.SingleOrDefault(x=>x.AuthorId == author.Id);
            if (author is null)
                throw new InvalidOperationException("Yazar mevcut değil");
            else if(book is not null)
                 throw new InvalidOperationException("Kitabı mevcut olan bir yazar silinemez.Yazarı silmek için lütfen önce kitabını siliniz.");
            _dbcontext.Authors.Remove(author);
            _dbcontext.SaveChanges();
        }
    }
}