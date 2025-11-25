using aes.Data;
using aes.Repositories;
using aes.Repositories.HEP.Elektra;
using aes.Repositories.HEP.ODS;
using aes.Repositories.IRepository;
using aes.Repositories.IRepository.HEP;
using aes.Repositories.RacuniRepositories;
using aes.Repositories.RacuniRepositories.Elektra;
using aes.Repositories.RacuniRepositories.IRacuniRepository;
using aes.Repositories.RacuniRepositories.IRacuniRepository.Elektra;
using aes.Repositories.Stan;
using System;
using System.Threading.Tasks;

namespace aes.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRacuniElektraRepository RacuniElektra { get; private set; }
        public IRacuniElektraRateRepository RacuniElektraRate { get; private set; }
        public IRacuniElektraIzvrsenjeUslugeRepository RacuniElektraIzvrsenjeUsluge { get; private set; }
        public IRacuniHoldingRepository RacuniHolding { get; private set; }
        public IElektraKupacRepository ElektraKupac { get; private set; }
        public IStanRepository Stan { get; private set; }
        public IOdsRepository Ods { get; private set; }
        public IPredmetRepository Predmet { get; private set; }
        public IDopisRepository Dopis { get; private set; }
        public IStanUpdateRepository StanUpdate { get; private set; }
        public IObracunPotrosnjeRepository ObracunPotrosnje { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {

            _context = context;
            RacuniElektra = new RacuniElektraRepository(_context);
            RacuniElektraRate = new RacuniElektraRateRepository(_context);
            RacuniElektraIzvrsenjeUsluge = new RacuniElektraIzvrsenjeUslugeRepository(_context);
            RacuniHolding = new RacuniHoldingRepository(_context);
            ElektraKupac = new ElektraKupacRepository(_context);
            Stan = new StanRepository(_context, this);
            Ods = new OdsRepository(_context);
            Predmet = new PredmetRepository(_context);
            Dopis = new DopisRepository(_context);
            StanUpdate = new StanUpdateRepository(_context);
            ObracunPotrosnje = new ObracunPotrosnjeRepository(_context);
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
