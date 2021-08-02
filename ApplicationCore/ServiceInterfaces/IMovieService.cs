using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieDetailsResponseModel>> GetAllMovies();
        Task<List<MovieRatedResponseModel>> GetTopRated();
        Task<MovieReviewResponseModel> GetMovieReviewById(int id);

        Task<MovieCreateResponseModel> CreateMovie(MovieCreateRequestModel movieCreateRequest);
        Task<MovieCreateResponseModel> UpdateMovie(MovieCreateRequestModel movieCreateRequest);

    }
}
