using aes.Repositories.IRepository;
using aes.Repositories.IRepository.HEP;
using aes.Repositories.RacuniRepositories.IRacuniRepository;
using aes.Repositories.RacuniRepositories.IRacuniRepository.Elektra;
using System;
using System.Threading.Tasks;

namespace aes.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRacuniElektraRepository RacuniElektra { get; }
        IRacuniElektraRateRepository RacuniElektraRate { get; }
        IRacuniElektraIzvrsenjeUslugeRepository RacuniElektraIzvrsenjeUsluge { get; }
        IStanRepository Stan { get; }
        IOdsRepository Ods { get; }
        IPredmetRepository Predmet { get; }
        IRacuniHoldingRepository RacuniHolding { get; }
        IDopisRepository Dopis { get; }
        IElektraKupacRepository ElektraKupac { get; }
        IStanUpdateRepository StanUpdate { get; }
        IObracunPotrosnjeRepository ObracunPotrosnje { get; }

        Task<int> Complete();
    }
}
