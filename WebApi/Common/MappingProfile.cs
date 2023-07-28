using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBookById;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static WebApi.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
        }
    }
}