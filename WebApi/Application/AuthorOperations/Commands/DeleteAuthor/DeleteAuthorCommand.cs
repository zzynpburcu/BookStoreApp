using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{

    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbcontext;
        public int ID {get;set;}
        public DeleteAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Handle()
        {
            var author = _dbcontext.Authors.SingleOrDefault(x => x.Id == ID);
             if (author is null)
                throw new InvalidOperationException("Yazar mevcut değil");

            var book = _dbcontext.Books.FirstOrDefault(x=>x.AuthorId == author.Id);
            if(book is not null)
                 throw new InvalidOperationException("Kitabı mevcut olan bir yazar silinemez.Yazarı silmek için lütfen önce kitabını siliniz.");
           
            _dbcontext.Authors.Remove(author);
            _dbcontext.SaveChanges();
        }
    }
}