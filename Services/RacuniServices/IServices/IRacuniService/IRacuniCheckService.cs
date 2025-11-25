using aes.Models.Racuni;
using System.Collections.Generic;

namespace aes.Services.RacuniServices.IServices.IRacuniService
{
    public interface IRacuniCheckService
    {
        string CheckIfExistsInPayed(string brojRacuna, IEnumerable<Racun> Racuni);
        string CheckIfExistsInTemp(string brojRacuna, IEnumerable<Racun> tempRacuni);
    }
}
