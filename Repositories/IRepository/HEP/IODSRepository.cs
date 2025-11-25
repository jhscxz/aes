using aes.Models.HEP;
using aes.Models.Racuni.Elektra;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aes.Repositories.IRepository.HEP
{
    public interface IOdsRepository : IRepository<Ods>
    {
        Task<IEnumerable<Ods>> GetAllOds();
        Task<IEnumerable<TRacun>> GetRacuniForOmm<TRacun>(int stanId) where TRacun : Elektra;
        Task<Ods> IncludeApartment(Ods ods);
    }
}
