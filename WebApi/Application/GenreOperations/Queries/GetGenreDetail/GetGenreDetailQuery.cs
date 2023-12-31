using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using System.Collections.Generic;
using System;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDbContext context = null, IMapper mapper = null)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.ID == GenreId);
            if( genre is null)
             throw new InvalidOperationException("Kitap türü bulunamadı");
             
            return _mapper.Map<GenreDetailViewModel>(genre);;
        }

        public class GenreDetailViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
    }
}