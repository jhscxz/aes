using aes.Models;
using aes.Models.Racuni.Holding;
using aes.Repositories.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aes.Repositories.RacuniRepositories.IRacuniRepository
{
    public interface IRacuniHoldingRepository : IRepository<RacunHolding>
    {
#nullable enable
        Task<Models.Stan?> GetStanBySifraObjekta(long sifraObjekta);
#nullable disable
        Task<IEnumerable<RacunHolding>> GetRacuni(int predmetId, int dopisId);
        Task<IEnumerable<RacunHolding>> GetRacuniForStan(int stanId);
        Task<IEnumerable<RacunHolding>> GetRacuniHoldingWithDopisiAndPredmeti();
        Task<IEnumerable<Predmet>> GetPredmetiForCreate();
        Task<IEnumerable<Dopis>> GetDopisiForPayedRacuni(int predmetId);
        Task<IEnumerable<RacunHolding>> TempList(string userId);
#nullable enable
        Task<RacunHolding?> IncludeAll(int id);
    }
}