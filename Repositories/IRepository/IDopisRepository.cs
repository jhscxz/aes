using aes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aes.Repositories.IRepository
{
    public interface IDopisRepository : IRepository<Dopis>
    {
        Task<IEnumerable<Dopis>> GetDopisiForPredmet(int predmetId);
        Task<IEnumerable<Dopis>> GetOnlyEmptyDopisiAsync(int predmetId);
        Task<Dopis> IncludePredmetAsync(Dopis dopis);
    }
}