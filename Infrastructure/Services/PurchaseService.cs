using ApplicationCore.Entities;
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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<IEnumerable<MovieCardResponseModel>> GetAllPurchasedMovie(int userId)
        {
            var movies = await _purchaseRepository.GetPurchasedMovieByUser(userId);
            var response = movies.Select(m => new MovieCardResponseModel
            { Id = m.Id, Title = m.Title, PosterUrl = m.PosterUrl, Budget = m.Budget.GetValueOrDefault() });
            return response;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchases()
        {
            var purchases = await _purchaseRepository.ListAllAsync();
            return purchases;
        }
    }
}
