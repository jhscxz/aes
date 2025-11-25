using aes.Services.RacuniServices.RacuniHoldingService.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using aes.UnitOfWork;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aes.Services.RacuniServices.RacuniHoldingService
{
    public class RacuniHoldingUploadService : IRacuniHoldingUploadService
    {
        private readonly IRacuniHoldingTempCreateService _RacuniHoldingTempCreateService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        //TODO: optimize upload service
        public RacuniHoldingUploadService(IRacuniHoldingTempCreateService RacuniHoldingTempCreateService,
            IUnitOfWork unitOfWork, ILogger logger)
        {
            _RacuniHoldingTempCreateService = RacuniHoldingTempCreateService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<JsonResult> Upload(HttpRequest Request, string userId)
        {
            string _loggerTemplate = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + ", " + "UserId: " + userId + ", " + "msg: ";

            IFormFileCollection files = Request.Form.Files;

            int tempListCount = (await _unitOfWork.RacuniHolding.TempList(userId)).Count();
            if (tempListCount >= 500)
            {
                string message = "U tablici ne može biti više od 500 računa!";
                _logger.Information(_loggerTemplate + message);
                return new JsonResult(new { success = false, message });
            }

            foreach (IFormFile file in files)
            {
                if (!file.ContentType.Equals("text/csv") || file.Length > 256000)
                {
                    string message = !file.ContentType.Equals("text/csv") ? "not .csv file" : "File too large, max 256 kb";
                    _logger.Information(_loggerTemplate + message);
                    return new JsonResult(new { success = false, message });
                }
                try
                {
                    using (StreamReader reader = new(file.OpenReadStream()))
                    {
                        double iznos = 0;

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (line == "EUR")
                            {
                                iznos = double.Parse(reader.ReadLine()) / 100;
                                for (int i = 0; i < 3; i++)
                                {
                                    _ = reader.ReadLine();
                                }
                                line = reader.ReadLine();
                                if (line != "ZAGREBAÄ\u008dKI HOLDING D.O.O.")
                                {
                                    throw new Exception("Nije račun Zagrebačkog holdinga");
                                }
                                for (int i = 0; i < 3; i++) // skipping 3 lines
                                {
                                    _ = reader.ReadLine();
                                }
                                line = reader.ReadLine();
                            }
                            if (line == "HR01")
                            {
                                _ = await _RacuniHoldingTempCreateService.AddNewTemp(reader.ReadLine(), iznos.ToString(), null, userId);
                                for (int i = 0; i < 4; i++) // skipping 4 lines
                                {
                                    _ = reader.ReadLine();
                                }
                            }
                        }
                    };
                }
                catch (Exception e)
                {
                    _logger.Error(_loggerTemplate + e.Message);
                    return new JsonResult(new { success = false, message = e.Message });
                }
            }

            _logger.Information(_loggerTemplate + "Uspješno uploadano");
            return new JsonResult(new { success = true, message = "Uspješno uploadano" });

        }
    }
}

