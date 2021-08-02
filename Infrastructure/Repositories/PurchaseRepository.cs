using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EFRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<bool> CheckMoviePurchaseForUser(int userId, int movieId)
        {
            var isPurchased = await _dbContext.Purchases.Where(p => p.MovieId == movieId && p.UserId == userId).AnyAsync();

            return isPurchased;
        }

        public override async Task<Purchase> GetByIdAsync(int id)
        {
            var purchase = await _dbContext.Purchases.Include(p => p.Movie).FirstOrDefaultAsync(p => p.UserId == id);
            return purchase;
        }

        public async Task<bool> GetExistsAsync(Expression<Func<Purchase, bool>> filter = null)
        {
            return filter != null && await _dbContext.Purchases.Where(filter).AnyAsync();
        }

        public async Task<List<Movie>> GetPurchasedMovieByUser(int userId)
        {
            
            var movies = await _dbContext.Purchases.Where(p => p.UserId == userId).Include(p => p.Movie).Select(p => p.Movie).ToListAsync();
            return movies;
        }
    }
}
