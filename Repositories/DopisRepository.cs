using aes.Data;
using aes.Models;
using aes.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aes.Repositories
{
    public class DopisRepository : Repository<Dopis>, IDopisRepository
    {
        public DopisRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Dopis>> GetOnlyEmptyDopisiAsync(int predmetId)
        {
            IEnumerable<Dopis> dopisi = await Find(e => e.PredmetId == predmetId);

            List<int> racuniElektraDopisIds = await Context.RacunElektra.Select(e => e.DopisId).Where(id => id.HasValue).Select(id => id.Value).ToListAsync();
            List<int> racuniElektraRateDopisIds = await Context.RacunElektraRate.Select(e => e.DopisId).Where(id => id.HasValue).Select(id => id.Value).ToListAsync();
            List<int> racuniElektraIzvrsenjeUslugeDopisIds = await Context.RacunElektraIzvrsenjeUsluge.Select(e => e.DopisId).Where(id => id.HasValue).Select(id => id.Value).ToListAsync();
            List<int> racuniHoldingDopisIds = await Context.RacunHolding.Select(e => e.DopisId).Where(id => id.HasValue).Select(id => id.Value).ToListAsync();

            HashSet<int> allRacuniDopisIds = new(racuniElektraDopisIds
                .Concat(racuniElektraRateDopisIds)
                .Concat(racuniElektraIzvrsenjeUslugeDopisIds)
                .Concat(racuniHoldingDopisIds));

            return dopisi.Where(d => !allRacuniDopisIds.Contains(d.Id))
                .OrderByDescending(d => d.Datum);
        }

        public async Task<IEnumerable<Dopis>> GetDopisiForPredmet(int predmetId)
        {
            return await Context.Dopis
                .Include(e => e.Predmet)
                .Where(e => e.PredmetId == predmetId)
                .OrderBy(e => e.Datum)
                .ToListAsync();
        }

        public async Task<Dopis> IncludePredmetAsync(Dopis dopis)
        {
            dopis.Predmet = await Context.Predmet.FirstOrDefaultAsync(e => e.Id == dopis.PredmetId);
            return dopis;
        }
    }
}