using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.TestsSetup
{
    public static class Books
    {
        public static void AddBooks(this ApplicationDbContext context)
        {
            context.Books.AddRange(
          new Book { GenreId = 1, Title = "Gözlerini Sımsıkı Kapat", PageCount = 400, PublishYear = new DateTime(2011, 12, 10) },
          new Book { GenreId = 1, Title = "Aklından Bir Sayı Tut", PageCount = 400, PublishYear = new DateTime(2001, 12, 15) },
          new Book { GenreId = 1, Title = "Pembe Mezarlık", PageCount = 400, PublishYear = new DateTime(2001, 12, 11) });
        }
    }
}
