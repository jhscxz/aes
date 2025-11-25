using aes.Models.Racuni.Elektra;
using aes.Services.RacuniServices.IServices.IRacuniService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aes.Services.RacuniServices.Elektra.RacuniElektra.Is
{
    public interface IRacuniElektraService : IRacuniervice
    {
        Task<IEnumerable<RacunElektra>> GetCreateRacuni(string userId);
        Task<IEnumerable<RacunElektra>> GetList(int predmetIdAsInt, int dopisIdAsInt);
    }
}