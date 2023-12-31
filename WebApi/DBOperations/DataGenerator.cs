using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                        new Genre{
                            Name = "Personal Growth"
                        },
                         new Genre{
                            Name = "Science Fiction"
                        },
                         new Genre{
                            Name = "Romance"
                        }
                );

                context.Books.AddRange(
                    new Book
                    {
                       // Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1, //Personal growth
                        PageCount = 200,
                        AuthorId = 2,
                        PublishDate = new DateTime(2001, 06, 12)
                    },
                     new Book
                     {
                       //  Id = 2,
                         Title = "Herland",
                         GenreId = 2, //Science Fiction
                         PageCount = 250,
                         AuthorId = 3,
                         PublishDate = new DateTime(2010, 05, 23)
                     },
                     new Book
                     {
                       //  Id = 3,
                         Title = "Dune",
                         GenreId = 1, //Science Fiction
                         PageCount = 540,
                         AuthorId = 1,
                         PublishDate = new DateTime(2001, 12, 21)
                     });
                     context.Authors.AddRange(
                         new Author{
                            Name="Stefan",
                            Surname="Zweig",
                            BirthDate=new DateTime(1081, 05, 22)
                         }  ,
                         new Author{
                            Name="Lev Nikolayeviç",
                            Surname="Tolstoy",
                            BirthDate=new DateTime(1953, 09, 08)
                         }   ,
                           new Author{
                            Name="Cemal",
                            Surname="Süreya",
                            BirthDate=new DateTime(1965, 01, 14)
                         }  
                     );

                     context.SaveChanges();
            }
        }
    }
}