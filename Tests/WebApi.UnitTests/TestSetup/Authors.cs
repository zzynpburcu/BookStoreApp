using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
            new Author { Name = "Stefan", Surname = "Zweig", BirthDate = new DateTime(1081, 05, 22) },
            new Author { Name = "Lev Nikolayeviç", Surname = "Tolstoy", BirthDate = new DateTime(1953, 09, 08) },
            new Author { Name = "Cemal", Surname = "Süreya", BirthDate = new DateTime(1965, 01, 14) });
        }
    }
}