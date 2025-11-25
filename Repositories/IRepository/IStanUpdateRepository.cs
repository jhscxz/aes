using aes.Models;

namespace aes.Repositories.IRepository
{
    public interface IStanUpdateRepository : IRepository<StanUpdate>
    {
#nullable enable
        StanUpdate? GetLatestAsync();
        StanUpdate? GetLatestSuccessfulUpdateAsync();
    }
}