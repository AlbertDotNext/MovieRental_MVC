using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IReviewRepository _reviewRepository;
        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, ICurrentUser currentUser, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _currentUser = currentUser;
            _reviewRepository = reviewRepository;
        }

        public async Task<Favorite> FavoriteMovie(int movieId, int userId)
        {
            var isLiked = await IsMovieFavoriteByUser(userId, movieId);
            if (isLiked) return null;
            var favorite = new Favorite { MovieId = movieId, UserId = userId };
            return await _favoriteRepository.AddAsync(favorite);
        }
        public async Task RemoveFavorite(UserFavoriteRequestModel favoriteRequest)
        {
            
            var dbFavorite = await _favoriteRepository.ListAsync(f => f.UserId == favoriteRequest.UserId
                                                                    && f.MovieId == favoriteRequest.MovieId);
            
            await _favoriteRepository.DeleteAsync(dbFavorite.First());

        }
        public async Task<bool> IsMovieFavoriteByUser(int userId, int movieId)
        {
            var isFavorite = await _favoriteRepository.GetExistsAsync(f => f.MovieId == movieId && f.UserId == userId);
            return isFavorite;
        }

        public async Task<Purchase> PurchaseMovie(UserPurchaseRequestModel requestModel)
        {
            var isPurchased = await _purchaseRepository.CheckMoviePurchaseForUser(requestModel.UserId, requestModel.MovieId);

            if (isPurchased)
            {
                throw new Exception("Movie already purchased");
            }
            var purchase = new Purchase
            {
                UserId = requestModel.UserId,
                MovieId = requestModel.MovieId,
                PurchaseDateTime = DateTime.Now,
                PurchaseNumber = Guid.NewGuid()
            };
            return await _purchaseRepository.AddAsync(purchase);
        }

        public async Task<List<UserResponseModel>> GetAllUsers()
        {
            var users = await _userRepository.ListAllAsync();
            var usersList = new List<UserResponseModel>();
            foreach (var user in users)
            {

                usersList.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth
                });
            }

            return usersList;
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var userResponseModel = new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userResponseModel;
        }

        public async Task<UserLoginResponseModel> Login(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmail(email);
            if (dbUser == null)
            {
                throw new NotFoundException("Email does not exists, please register first");
            }

            var hashedPssword = HashPassword(password, dbUser.Salt);

            if (hashedPssword == dbUser.HashedPassword)
            {
                // good, correct password

                var userLoginRespone = new UserLoginResponseModel
                {

                    Id = dbUser.Id,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    DateOfBirth = dbUser.DateOfBirth,
                    LastName = dbUser.LastName
                };

                return userLoginRespone;
            }

            return null;
        }


        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // Make sure email does not exists in database User table

            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);

            if (dbUser != null)
            {
                // we already have user with same email
                throw new ConflictException("Email arleady exists");
            }

            // create a unique salt

            var salt = CreateSalt();

            var hashedPassword = HashPassword(requestModel.Password, salt);

            // save to database

            var user = new User
            {
                Email = requestModel.Email,
                Salt = salt,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                HashedPassword = hashedPassword
            };

            // save to database by calling UserRepository Add method
            var createdUser = await _userRepository.AddAsync(user);

            var userResponse = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userResponse;
        }
        private string CreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string HashPassword(string password, string salt)
        {
            // Aarogon
            // Pbkdf2
            // BCrypt
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                    password: password,
                                                                    salt: Convert.FromBase64String(salt),
                                                                    prf: KeyDerivationPrf.HMACSHA512,
                                                                    iterationCount: 10000,
                                                                    numBytesRequested: 256 / 8));
            return hashed;
        }

        public async Task<List<Movie>> GetUserFavoriteMovies(int userId)
        {
            var movies = await _userRepository.GetUserFavoriteMoviesAsync(userId);
            return movies;
        }

        public async Task<IEnumerable<UserReviewedMovieResponseModel>> GetUserReviewedMovies(int userId)
        {
            var reviews = await _reviewRepository.GetUserReviewedMovies(userId);
            if (reviews == null) throw new Exception("You haven't reviewed any movies...");
            var response = reviews.Select(r => new UserReviewedMovieResponseModel
            {
                UserId = r.UserId,
                MovieId = r.MovieId,
                Title = r.Movie.Title,
                PosterUrl = r.Movie.PosterUrl,
                ReviewText = r.ReviewText,
                Rating = r.Rating,
                FirstName = r.User.FirstName,
                LastName = r.User.LastName,
                
            });
            return response;
        }
    }
}
