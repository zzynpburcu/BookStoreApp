using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{

    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public UpdateAuthorModel Model { get; set; }
        public int ID {get; set;}
        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
    public void Handle()
    {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == ID);
            if (author is null)
                throw new InvalidOperationException("Kitap mevcut değil");

            author.Name = Model.Name != default ? Model.Name : author.Name;
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
            author.BirthDate = Model.Date != default ? Model.Date : author.BirthDate;

            _dbContext.SaveChanges();
    }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Date { get; set; }
    }
}