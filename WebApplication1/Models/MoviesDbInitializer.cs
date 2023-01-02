using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MoviesDbInitializer : DropCreateDatabaseAlways<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            context.Movies.Add(new Movie
            {
                Title = "Avatar 2",
                Author = "James Cameron",
                CreateBy = "Universal Pictures",
                Date = new DateTime(2022, 12, 16)
            });
            context.Movies.Add(new Movie
            {
                Title = "Terminator",
                Author = "Ерлан Калиакпаров",
                CreateBy = "20th century fox",
                Date = new DateTime(2018, 8, 8)
            });
            context.Movies.Add(new Movie
            {
                Title = "Snatch",
                Author = "James Cameron",
                CreateBy = "Universal Pictures",
                Date = new DateTime(2015, 4, 6)
            });
            context.Movies.Add(new Movie
            {
                Title = "One punch man",
                Author = "Юсукэ Мурата",
                CreateBy = "Madhouse",
                Date = new DateTime(2020, 5, 20)
            });

            base.Seed(context);
        }
    }
}