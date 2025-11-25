using aes.Models.HEP;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aes.Repositories.IRepository.HEP
{
    public interface IObracunPotrosnjeRepository : IRepository<ObracunPotrosnje>
    {
        Task<ObracunPotrosnje> GetLastForRacunId(int racunId);
        Task<IEnumerable<ObracunPotrosnje>> GetObracunForUgovorniRacun(long ugovorniRacun);
        Task<IEnumerable<ObracunPotrosnje>> GetObracunPotrosnjeForRacun(int racunId);
    }
}