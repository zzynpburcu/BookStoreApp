using System;
using System.Linq;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public  CreateAuthorModel Model { get; set;}
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault( x => x.Name == Model.Name);
            if( author is not null)
             throw new InvalidOperationException("Yazar zaten mevcut");

            author = new Author(); 
            author.Name = Model.Name;
            author.Surname = Model.Surname;
            author.BirthDate=Model.Date;
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime Date { get; set; }
    }
}