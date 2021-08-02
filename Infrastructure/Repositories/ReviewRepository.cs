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
    public class ReviewRepository : EFRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Review>> ListAsync(Expression<Func<Review, bool>> filter)
        {
            
            return await _dbContext.Reviews.Where(filter).Include(r => r.Movie).ToListAsync();
        }

        public async Task<List<Review>> GetUserReviewedMovies(int userId)
        {
            var reviewedMovies =
                await _dbContext.Reviews.Where(r => r.UserId == userId).Include(r => r.Movie).Include(r => r.User).ToListAsync();
            return reviewedMovies;
        }

        public async Task<Review> IsReviewedByUserAsync(int userId, int movieId)
        {
            return await _dbContext.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
        }
    }
}
