using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;
        public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }

        public async Task<MovieCreateResponseModel> CreateMovie(MovieCreateRequestModel movieCreateRequest)
        {
            var dbMovie = await _movieRepository.GetByIdAsync(movieCreateRequest.Id);

            if (dbMovie != null)
            {
                
                throw new Exception("Movie arleady exists");
            }
            var movie = new Movie
            {
                Id = movieCreateRequest.Id,
                Title = movieCreateRequest.Title,
                Overview = movieCreateRequest.Overview,
                Tagline = movieCreateRequest.Tagline,
                Budget = movieCreateRequest.Budget,
                Revenue = movieCreateRequest.Revenue,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                PosterUrl = movieCreateRequest.PosterUrl,
                BackdropUrl = movieCreateRequest.BackdropUrl,
                OriginalLanguage = movieCreateRequest.OriginalLanguage,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                RunTime = movieCreateRequest.RunTime,
                Price = movieCreateRequest.Price,

            };
            var createdMovie = await _movieRepository.AddAsync(movie);
            var movieResponse = new MovieCreateResponseModel
            {
                MovieId = createdMovie.Id,
                Title = createdMovie.Title
            };
            return movieResponse;
        }

        public async Task<MovieCreateResponseModel> UpdateMovie(MovieCreateRequestModel movieCreateRequest)
        {
            var dbMovie = await _movieRepository.GetByIdAsync(movieCreateRequest.Id);

            if (dbMovie == null)
            {

                throw new Exception("No Movie exists");
            }
            var movie = new Movie
            {
                Id = movieCreateRequest.Id,
                Title = movieCreateRequest.Title,
                Overview = movieCreateRequest.Overview,
                Tagline = movieCreateRequest.Tagline,
                Budget = movieCreateRequest.Budget,
                Revenue = movieCreateRequest.Revenue,
                ImdbUrl = movieCreateRequest.ImdbUrl,
                TmdbUrl = movieCreateRequest.TmdbUrl,
                PosterUrl = movieCreateRequest.PosterUrl,
                BackdropUrl = movieCreateRequest.BackdropUrl,
                OriginalLanguage = movieCreateRequest.OriginalLanguage,
                ReleaseDate = movieCreateRequest.ReleaseDate,
                RunTime = movieCreateRequest.RunTime,
                Price = movieCreateRequest.Price,

            };
            var createdMovie = await _movieRepository.UpdateAsync(movie);
            var movieResponse = new MovieCreateResponseModel
            {
                MovieId = createdMovie.Id,
                Title = createdMovie.Title
            };
            return movieResponse;
        }
        public async Task<List<MovieDetailsResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.ListAllAsync();

            var movieRetrive = new List<MovieDetailsResponseModel>();
            foreach (var movie in movies)
            {
                movieRetrive.Add(new MovieDetailsResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                    BackdropUrl = movie.BackdropUrl,
                    Overview = movie.Overview,
                    Tagline = movie.Tagline,
                    Budget = movie.Budget,
                    Revenue = movie.Revenue,
                    ImdbUrl = movie.ImdbUrl,
                    TmdbUrl = movie.TmdbUrl,
                    RunTime = movie.RunTime,
                    Price = movie.Price,
                    ReleaseDate = movie.ReleaseDate,

                });
            }
            return movieRetrive;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            var movieDetails = new MovieDetailsResponseModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Rating = movie.Rating.GetValueOrDefault(),
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget.GetValueOrDefault(),
                Revenue = movie.Revenue.GetValueOrDefault(),
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                RunTime = movie.RunTime,
                Price = movie.Price

            };

            movieDetails.Casts = new List<CastResponseModel>();

            foreach (var cast in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Name = cast.Cast.Name,
                    Gender = cast.Cast.Gender,
                    TmdbUrl = cast.Cast.TmdbUrl,
                    ProfilePath = cast.Cast.ProfilePath,
                    Character = cast.Character,
                });
            }

            movieDetails.Genres = new List<GenreModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(
                    new GenreModel
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    }
                    );
            }

            return movieDetails;
        }

        public async Task<MovieReviewResponseModel> GetMovieReviewById(int id)
        {
            var movie = await _movieRepository.GetMoviesReview(id);
            var movieReview = new MovieReviewResponseModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                PosterUrl = movie.PosterUrl
            };
            movieReview.Reviews = new List<ReviewModel>();
            foreach (var review in movie.Reviews)
            {
                movieReview.Reviews.Add(new ReviewModel
                {
                    UserId = review.UserId,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }
            return movieReview;
        }

        public async Task<List<MovieRatedResponseModel>> GetTopRated()
        {
            var movies = await _movieRepository.GetHighest50RatedMovies();
            var response = new List<MovieRatedResponseModel>();
            foreach (var movie in movies)
            {
                response.Add(new MovieRatedResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl,
                    Rating = movie.Rating
                });
            }
                
            return response;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            var movies = await _movieRepository.GetHighest30GrossingMovies();
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

        
    }
}
