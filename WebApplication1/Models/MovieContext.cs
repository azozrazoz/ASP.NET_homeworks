using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("DefaultConnection") { }

        public DbSet<Movie> Movies { get; set;}


        public DbSet<BuyTickets> Tickets { get; set;}
    }

    public interface IRepository
    {
        void Save(Movie m);
        void Remove(int? id);
        IEnumerable<Movie> List();
        Movie Get(int? id);
    }

    public class MovieRepository : IDisposable, IRepository
    {
        private MovieContext db = new MovieContext();

        public MovieRepository(MovieContext context)
        {
            db = context;
        }

        public void Save(Movie m)
        {
            db.Movies.Add(m);
            db.SaveChanges();
        }
        public IEnumerable<Movie> List()
        {
            return db.Movies;
        }

        public Movie Get(int? id)
        {
            if (id == null)
                return null;

            return db.Movies.Find(id);
        }

        public void Remove(int? id)
        {
            if (id == null)
                return;

            Movie movie = db.Movies.Find(id); 
            db.Movies.Remove(movie);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}