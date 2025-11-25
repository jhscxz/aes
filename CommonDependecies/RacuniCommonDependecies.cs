using aes.CommonDependecies.ICommonDependencies;
using aes.Models.Datatables;
using aes.Services.RacuniServices.IServices;
using aes.Services.RacuniServices.IServices.IRacuniService;
using aes.UnitOfWork;

namespace aes.CommonDependecies
{
    public class RacuniCommonDependecies : CommonDependencies, IRacuniCommonDependecies
    {
        public IRacuniTempEditorService RacuniTempEditorService { get; }
        public IRacuniCheckService RacuniCheckService { get; }
        public IService Service { get; }

        public RacuniCommonDependecies(IDatatablesGenerator datatablesGenerator, IDatatablesSearch datatablesSearch, IUnitOfWork unitOfWork,
             IRacuniTempEditorService racuniTempEditorService, IService service,
            IRacuniCheckService racuniCheckService)
            : base(datatablesGenerator, datatablesSearch, unitOfWork)
        {
            RacuniTempEditorService = racuniTempEditorService;
            RacuniCheckService = racuniCheckService;
            Service = service;
        }
    }
}
