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
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<ApplicationCore.Entities.Cast> GetCastById(int id)
        {
            var cast = await _castRepository.GetByIdAsync(id);
            return cast;
        }

        public async Task<CastResponseModel> GetCastDetails(int id)
        {
            var casts = await _castRepository.GetCasts(id);
            var castDetails = new CastResponseModel()
            {
                Id = casts.Id,
                Name = casts.Name,
                Gender = casts.Gender,
                TmdbUrl = casts.TmdbUrl,
                ProfilePath = casts.ProfilePath,
                
            };

            castDetails.Casts = new List<CastResponseModel>();

            foreach (var character in casts.MovieCasts)
            {
                castDetails.Casts.Add(new CastResponseModel
                {
                    Character = character.Character,
                });
            }


            return castDetails;
        }

    }
}
