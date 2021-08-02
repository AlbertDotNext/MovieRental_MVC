using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<List<Movie>> GetHighest30GrossingMovies();
        Task<List<Movie>> GetHighest50RatedMovies();
        Task<Movie> GetMoviesReview(int id);
    }
}
