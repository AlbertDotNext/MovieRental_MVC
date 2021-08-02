using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenreRepository : EFRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            var genres = await _dbContext.Genres.OrderBy(g => g.Name).ToListAsync();
            return genres;
        }
        public override async Task<Genre> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Genres.Include(g => g.Movies.Take(50)).FirstOrDefaultAsync(g => g.Id == id);

            if (movie == null)
            {
                throw new Exception($"No Movie Found with {id}");
            }

            return movie;
            
        }

        public async Task<List<Movie>> GetMovieByGenre(int id)
        {
            //var movies = await _dbContext.Movies.Include(m => m.Genres.Where(g => g.Id == id)).Take(200).ToListAsync();

            var genre = await _dbContext.Genres.Include(g => g.Movies).FirstOrDefaultAsync(g => g.Id == id);
            var movies = genre.Movies.ToList();

            return movies;
        }
    }
}
