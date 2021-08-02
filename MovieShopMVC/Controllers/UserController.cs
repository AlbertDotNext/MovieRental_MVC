using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        public UserController(ICurrentUser currentUser, IUserRepository userRepository, IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _currentUser = currentUser;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }
        
        //public async Task<IActionResult> MovieLibrary(int id)
        //{
        //    return View();
        //}
        public async Task<IActionResult> ConfirmPurchase(int id)
        {

            if (_currentUser.IsAuthenticated)
            {
                var userId = _currentUser.UserId;
                var user = await _userRepository.GetByIdAsync(id);
                var movie = await _movieRepository.GetByIdAsync(id);
                var purchase = new Purchase
                {
                    UserId = user.Id,
                    TotalPrice = movie.Price.GetValueOrDefault(),
                    MovieId = id,
                    PurchaseDateTime = DateTime.Now,
                    PurchaseNumber = Guid.NewGuid()
                };

                var createdPurchase = await _purchaseRepository.AddAsync(purchase);
                purchase.Id = createdPurchase.Id;
                return View("Successfully");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}
