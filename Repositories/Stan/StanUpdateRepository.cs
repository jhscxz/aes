using aes.Data;
using aes.Models;
using aes.Repositories.IRepository;
using System.Linq;

namespace aes.Repositories.Stan
{
    public class StanUpdateRepository : Repository<StanUpdate>, IStanUpdateRepository
    {
        public StanUpdateRepository(ApplicationDbContext context) : base(context) { }

        /// <summary>
        /// Gets the latest StanUpdate based on when the update began.
        /// </summary>
        /// <returns>The most recently started StanUpdate.</returns>
#nullable enable
        public StanUpdate? GetLatestAsync()
#nullable disable
        {
            return Context.StanUpdate
                    .OrderByDescending(e => e.UpdateBegan)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Gets the latest 'successful' StanUpdate based on DateOfData.
        /// A successful update here is assumed to be the latest by DateOfData.
        /// </summary>
        /// <returns>The StanUpdate with the most recent DateOfData.</returns>
#nullable enable
        public StanUpdate? GetLatestSuccessfulUpdateAsync()
#nullable disable
        {
            return Context.StanUpdate
                .OrderByDescending(e => e.DateOfData)
                .FirstOrDefault();
        }
    }
}
