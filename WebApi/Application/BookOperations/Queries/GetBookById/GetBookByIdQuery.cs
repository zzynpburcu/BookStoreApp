using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Queries.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int ID {get; set;}
        public GetBookByIdQuery(BookStoreDbContext _context, IMapper mapper)
        {
            _dbContext = _context;
            _mapper = mapper;
        }

        public BookDetailModel Handle()
        {
            var book = _dbContext.Books.Include(x => x.Genre).Where(book => book.Id == ID).SingleOrDefault();
            if (book is null)
               throw new InvalidOperationException("Kitap mevcut değil");
            BookDetailModel vm = _mapper.Map<BookDetailModel>(book);
                    
                
            return vm;
           
        }

    }
    public class BookDetailModel{
          public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }

    }
}