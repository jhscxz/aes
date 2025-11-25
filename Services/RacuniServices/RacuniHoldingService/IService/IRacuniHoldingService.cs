using aes.Models.Racuni.Holding;
using aes.Services.RacuniServices.IServices.IRacuniService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aes.Services.RacuniServices.RacuniHoldingService.IService
{
    public interface IRacuniHoldingService : IRacuniervice
    {
        Task<IEnumerable<RacunHolding>> GetCreateRacuni(string userId);
        Task<IEnumerable<RacunHolding>> GetList(int predmetIdAsInt, int dopisIdAsInt);
    }
}