using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<List<Movie>> GetUserFavoriteMoviesAsync(int userId)
        {
            var movies = await _dbContext.Favorites.Where(f => f.UserId == userId).Include(f => f.Movie)
                .Select(f => new Movie { Id = f.Movie.Id, Title = f.Movie.Title, PosterUrl = f.Movie.PosterUrl }).ToListAsync();
            return movies;
        }

        public override async Task<IEnumerable<User>> ListAllAsync()
        {
            var users = await _dbContext.Users.Take(200).ToListAsync();
            return users;
        }


    }
}
