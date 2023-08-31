using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using System.Collections.Generic;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context = null, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x => x.Id).ToList<Author>();
            List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authors);
            return vm;
        }

        public class AuthorsViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }
    }
}