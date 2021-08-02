using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);

        Task<UserLoginResponseModel> Login(string email, string password);

        Task<UserResponseModel> GetUserById(int id);
        Task<List<UserResponseModel>> GetAllUsers();

        Task<Purchase> PurchaseMovie(UserPurchaseRequestModel requestModel);
        Task<Favorite> FavoriteMovie(int movieId, int userId);
        Task<bool> IsMovieFavoriteByUser(int userId, int movieId);

        Task RemoveFavorite(UserFavoriteRequestModel favoriteRequest);
        Task<List<Movie>> GetUserFavoriteMovies(int userId);
        Task<IEnumerable<UserReviewedMovieResponseModel>> GetUserReviewedMovies(int userId);


    }
}
