using aes.Models;
using aes.Models.Racuni;
using System.Collections.Generic;

namespace aes.Repositories.IRepository
{
    public interface IPredmetRepository : IRepository<Predmet>
    {
        IEnumerable<Predmet> GetPredmetForAllPaidRacuni(IEnumerable<Racun> racuni);
    }
}