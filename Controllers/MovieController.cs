using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using testAPI.Models;
using System.IO;
// using Newtonsoft.Json;

namespace testAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        // Create a list of sample movies (10)
        public static List<Movie> movies = new List<Movie>
        {
            new Movie { Title = "Inception", Director = "Christopher Nolan", Genre = "Sci-Fi", Year = 2010, Rating = 8 },
            new Movie { Title = "The Shawshank Redemption", Director = "Frank Darabont", Genre = "Drama", Year = 1994, Rating = 9 },
            new Movie { Title = "Pulp Fiction", Director = "Quentin Tarantino", Genre = "Crime", Year = 1994, Rating = 8 },
            new Movie { Title = "The Dark Knight", Director = "Christopher Nolan", Genre = "Action", Year = 2008, Rating = 9 },
            new Movie { Title = "The Lord of the Rings: The Fellowship of the Ring", Director = "Peter Jackson", Genre = "Fantasy", Year = 2001, Rating = 9 },
            new Movie { Title = "Interstellar", Director = "Christopher Nolan", Genre = "Sci-Fi", Year = 2014, Rating = 9 },
            new Movie { Title = "Fight Club", Director = "David Fincher", Genre = "Drama", Year = 1999, Rating = 8 },
            new Movie { Title = "The Godfather", Director = "Francis Ford Coppola", Genre = "Crime", Year = 1972, Rating = 9 },
            new Movie { Title = "The Matrix", Director = "The Wachowskis", Genre = "Action", Year = 1999, Rating = 8 },
            new Movie { Title = "Forrest Gump", Director = "Robert Zemeckis", Genre = "Drama", Year = 1994, Rating = 9 }
        };

        // Endpoint to get list of all movies
        [HttpGet("")]
        public List<Movie> GetMovies([FromQuery] int? minYear, [FromQuery] int? minRating)
        {
            List<Movie> movieList = movies;
            if (minYear >= 0)
            {
                movieList = movieList.FindAll(m => m.Year >= minYear);
            }
            if (minRating >= 0)
            {
                movieList = movieList.FindAll(m => m.Rating >= minRating);
            }
            return movieList;
        }

        [HttpPost]
        [Route("addMovie")]
        public ActionResult<Movie> AddMovie(Movie movie)
        {
            movies.Add(movie);
            return Ok(movie);
        }

        // Endpoint to get Movie by Name
        [HttpGet("title/{title}")]
        public ActionResult<Movie> GetMoviesByName(string title)
        {
            Movie? movieToReturn = movies.FirstOrDefault(m => m.Title.ToLower() == title.ToLower());
            if (movieToReturn == null)
            {
                return BadRequest("Movie not found");
            }
            return Ok(movieToReturn);
        }

        // Endpoint to get movies by genre
        [HttpGet("genre/{genre}")]
        public ActionResult<List<Movie>> GetMoviesByGenre(string genre, [FromQuery] int? year)
        {
            List<Movie> movieList = movies.FindAll(m => m.Genre.ToLower() == genre.ToLower());
            if (movieList.Count == 0)
            {
                return BadRequest("Bad request: genre");
            }
            return Ok(movieList);
        }

        // Endpoint to get movies by year
        [HttpGet("year/{year}")]
        public ActionResult<List<Movie>> GetMoviesByYear(int year)
        {
            List<Movie> movieList = movies.FindAll(m => m.Year == year);
            if (movieList.Count == 0)
            {
                return BadRequest("Bad request: year");
            }
            return Ok(movieList);
        }

        // Endpoint to get movies by Rating
        [HttpGet("rating/{rating}")]
        public ActionResult<List<Movie>> GetMoviesByRating(int rating)
        {
            List<Movie> movieList = movies.FindAll(m => m.Rating == rating);
            if (movieList.Count == 0)
            {
                return BadRequest("Bad Request: rating");
            }
            return Ok(movieList);
        }
    }
}