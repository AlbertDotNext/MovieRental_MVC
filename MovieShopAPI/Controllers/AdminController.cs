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
    public class AdminController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IPurchaseService _purchaseService;
        public AdminController(IMovieService movieService, IPurchaseService purchaseService)
        {
            _movieService = movieService;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [Route("movie")]
        public async Task<IActionResult> CreateMovieByAdminAsync(MovieCreateRequestModel movieCreateRequest)
        {
            var createdMovie = await _movieService.CreateMovie(movieCreateRequest);
            
            return Ok(createdMovie);
        }
        [HttpPut]
        [Route("movie")]
        public async Task<IActionResult> UpdateMovie(MovieCreateRequestModel movieCreateRequest)
        {
            var updatedMovie = await _movieService.UpdateMovie(movieCreateRequest);
            return Ok(updatedMovie);
        }
        [HttpGet]
        [Route("purchase")]
        public async Task<IActionResult> GetAllPurchases()
        {
            var movies = await _purchaseService.GetAllPurchases();
            return Ok(movies);
        }
    }
}
