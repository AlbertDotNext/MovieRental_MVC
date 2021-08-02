using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IPurchaseRepository : IAsyncRepository<Purchase>
    {
        Task<Purchase> GetByIdAsync(int id);
        Task<bool> GetExistsAsync(Expression<Func<Purchase, bool>> filter = null);
        Task<List<Movie>> GetPurchasedMovieByUser(int userId);

        Task<bool> CheckMoviePurchaseForUser(int userId, int movieId);
    }
}
