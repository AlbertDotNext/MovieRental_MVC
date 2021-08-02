using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EFRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Movie>> GetHighest30GrossingMovies()
        {
            var topMovies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return topMovies;
        }
        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                throw new Exception($"No Movie Found with {id}");
            }

            var movieRating = await _dbContext.Reviews.Where(m => m.MovieId == id).DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);

            if (movieRating > 0)
            {
                movie.Rating = movieRating;
            }

            return movie;
        }
        public override async Task<IEnumerable<Movie>> ListAllAsync()
        {
            var movie = await _dbContext.Movies.Take(200).ToArrayAsync();

            return movie;
        }

        public async Task<List<Movie>> GetHighest50RatedMovies()
        {
            var movies = await _dbContext.Reviews.Include(r => r.Movie)
                .GroupBy(r => new { r.Movie.Id, r.Movie.PosterUrl, r.Movie.Title })
                .OrderByDescending(g => g.Average(r => r.Rating))
                .Select(m => new Movie
                {
                    Id = m.Key.Id,
                    PosterUrl = m.Key.PosterUrl,
                    Title = m.Key.Title,
                    Rating = m.Average(r => r.Rating)
                }).Take(50).ToListAsync();
            return movies;
        }

        public async Task<Movie> GetMoviesReview(int id)
        {
            var movieReviews = await _dbContext.Movies.Include(m => m.Reviews).FirstOrDefaultAsync(m => m.Id == id);
            return movieReviews;
        }
    }
}
