using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        private readonly IPurchaseService _purchaseService;
        private readonly IReviewService _reviewService;
        public UserController(ICurrentUser currentUser, IUserService userService, IPurchaseService purchaseService, IReviewService reviewService)
        {
            _currentUser = currentUser;
            _userService = userService;
            _purchaseService = purchaseService;
            _reviewService = reviewService;
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseMovie([FromBody] UserPurchaseRequestModel model)
        {
            
            var purchase = await _userService.PurchaseMovie(model);
            
            return Ok();
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> FavoriteMovie([FromBody] UserFavoriteRequestModel model)
        {
            
            var favorite = await _userService.FavoriteMovie(model.MovieId, model.UserId);
            if (favorite == null) return BadRequest(new { errorMessage = "You have already liked the movie." });
            return Ok();
        }

        [HttpPost]
        [Route("unfavorite/{movieId}")]
        public async Task<IActionResult> RemoveFavorite([FromBody] UserFavoriteRequestModel favoriteRequest)
        {
            await _userService.RemoveFavorite(favoriteRequest);
            return Ok();
            
        }

        [HttpGet]
        [Route("{Id:int}/movie/{movieId:int}/favorite")]
        public async Task<IActionResult> IsFavorite(int Id, int movieId)
        {
            var isFavorite = await _userService.IsMovieFavoriteByUser(Id, movieId);
            return Ok(new { IsFavorite = isFavorite });
        }

        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> WriteReview([FromBody] ReviewRequestModel model)
        {
            
            var review = await _reviewService.WriteReview(model);
            return Ok(review);
        }

        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewRequestModel model)
        {  
            var review = await _reviewService.UpdateReview(model);
            return Ok(review);
        }



        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetUserPurchasedMovies(int userId)
        {
            
            var purchases = await _purchaseService.GetAllPurchasedMovie(userId);
            return Ok(purchases);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetUserFavoriteMovies(int id)
        {
            
            var movies = await _userService.GetUserFavoriteMovies(id);
            return Ok(movies);
        }

        [HttpGet]
        [Route("reviews")]
        public async Task<IActionResult> GetUserReviewedMovies(int id)
        {
            
            var reviews = await _userService.GetUserReviewedMovies(id);
            return Ok(reviews);
        }


    }
}
