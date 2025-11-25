using aes.Services.RacuniServices.Elektra.RacuniElektra.Is;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using aes.UnitOfWork;
using System.Linq;
using System.IO;
using System;
using System.Threading.Tasks;

namespace aes.Services.RacuniServices.Elektra.RacuniElektra
{
    public class RacuniElektraUploadService : IRacuniElektraUploadService
    {
        private readonly IRacuniElektraTempCreateService _RacuniElektraTempCreateService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;


        //TODO: optimize upload service
        public RacuniElektraUploadService(IRacuniElektraTempCreateService RacuniElektraTempCreateService,
            IUnitOfWork unitOfWork, ILogger logger)
        {
            _RacuniElektraTempCreateService = RacuniElektraTempCreateService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<JsonResult> Upload(HttpRequest Request, string userId)
        {
            string _loggerTemplate = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + ", " + "UserId: " + userId + ", " + "msg: ";

            IFormFileCollection files = Request.Form.Files;

            int tempListCount = (await _unitOfWork.RacuniElektra.TempList(userId)).Count();

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
                        if (tempListCount >= 500)
                        {
                            string message = "U tablicu ne može dodati više od 500 računa odjednom!";
                            _logger.Information(_loggerTemplate + message);
                            return new JsonResult(new { success = false, message });
                        }

                        while (!reader.EndOfStream)
                        {
                            double iznos = 0;
                            string line = reader.ReadLine();

                            if (line == "EUR")
                            {
                                iznos = double.Parse(reader.ReadLine()) / 100;

                                SkipLines(reader, 3);

                                line = reader.ReadLine();

                                if (!line.Contains("HEP ELEKTRA"))
                                {
                                    throw new Exception("Nije račun HEP ELEKTRE");
                                }

                                SkipLines(reader, 3);

                                line = reader.ReadLine();
                            }

                            if (line == "HR01")
                            {
                                _ = await _RacuniElektraTempCreateService.AddNewTemp(reader.ReadLine(), iznos.ToString(), null, userId);
                                SkipLines(reader, 4);
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
        private static void SkipLines(StreamReader reader, int numberOfLines)
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                _ = reader.ReadLine();
            }
        }
    }
}
