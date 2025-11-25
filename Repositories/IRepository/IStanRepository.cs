using aes.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aes.Repositories.IRepository
{
    public interface IStanRepository : IRepository<Models.Stan>
    {
        ApplicationDbContext Context { get; }
        Task<IEnumerable<Models.Stan>> GetStanovi();
        Task<IEnumerable<Models.Stan>> GetStanoviWithoutOdsOmm();
        Task UpdateRange(IEnumerable<Models.Stan> stanoviZaUpdate);

    }
}