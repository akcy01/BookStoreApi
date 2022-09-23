using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Tests.TestsSetup
{
    public static class Genres
    {
        public static void AddGenres(this ApplicationDbContext context)
        {
            context.Genres.AddRange(
                new Genre
                {
                    Name = "Korku"
                },
                new Genre
                {
                    Name = "Komedi"
                },
                new Genre
                {
                    Name = "Gerilim"
                }
                );
        }
    }
}
