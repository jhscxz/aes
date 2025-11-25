using aes.Data;
using Microsoft.EntityFrameworkCore;
using aes.UnitOfWork;
using aes.Repositories.IRepository;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace aes.Repositories.Stan
{
    public class StanRepository : Repository<Models.Stan>, IStanRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public new ApplicationDbContext Context { get; }

        private static readonly Func<ApplicationDbContext, int, IEnumerable<Models.Stan>> CompiledQuery
            = EF.CompileQuery((ApplicationDbContext dbContext, int id) => dbContext.Stan.Where(e => e.Id != id));

        public StanRepository(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            Context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Models.Stan>> GetStanovi()
        {
            // HACK: dummy entity
            // return await _unitOfWork.Stan.Find(e => e.Id != 25265);
            ApplicationDbContext dbContext = _unitOfWork.Stan.Context;
            List<Models.Stan> result = CompiledQuery(dbContext, 25265).ToList();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Models.Stan>> GetStanoviWithoutOdsOmm()
        {
            return await Context.Stan
                .Where(s => !Context.Ods.Any(o => o.StanId == s.Id))
                .ToListAsync();
        }

        public async Task UpdateRange(IEnumerable<Models.Stan> stanoviZaUpdate)
        {
            ApplicationDbContext dbContext = _unitOfWork.Stan.Context;
            dbContext.Stan.UpdateRange(stanoviZaUpdate);
            _ = await dbContext.SaveChangesAsync();
        }
    }
}