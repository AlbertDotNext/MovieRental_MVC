using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<MovieCardResponseModel>> GetGenreMovies(int id)
        {
            var movies = await _genreRepository.GetMovieByGenre(id);

            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
               
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }

            return movieCards;
        }

        public async Task<List<GenreModel>> GetGenres()
        {
            var genres = await _genreRepository.GetAllGenres();
            var genreList = new List<GenreModel>();
            foreach (var genre in genres)
            {
                genreList.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }

            return genreList;
            
        }

        
    }
}
