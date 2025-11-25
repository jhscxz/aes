using aes.Models;
using aes.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Globalization;
using aes.UnitOfWork;
using System.Threading.Tasks;
using System;
using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;

namespace aes.Services
{
    public class StanUploadService : IStanUploadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;


        public StanUploadService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<JsonResult> Upload(HttpRequest request, string userName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string loggerTemplate = System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType!.FullName + ", " + "User: " + userName + ", " + "msg: ";

            {
                StanUpdate stanUpdate = _unitOfWork.StanUpdate.GetLatestAsync();
            }

            IFormFileCollection files;

            try
            {
                files = request.Form.Files;
            }
            catch (Exception e)
            {

                _logger.Error(loggerTemplate + e.Message);

                return new JsonResult(new
                {
                    success = false,
                    message = e.Message
                });
            }

            foreach (IFormFile file in files)
            {
                // Check for XLSX file
                if (!file.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    string message1 = "not .xlsx file";
                    _logger.Information(loggerTemplate + message1);

                    return new JsonResult(new
                    {
                        success = false,
                        message1
                    });
                }

                try
                {
                    // Process the XLSX file
                    using MemoryStream stream = new();
                    await file.CopyToAsync(stream);
                    using ExcelPackage package = new(stream);

                    ExcelWorksheet worksheet = package.Workbook.Worksheets["StambenoUpravljanje"];
                    int rowCount = worksheet.Dimension.Rows;

                    DateTime dateTimeOfData = new();
                    NumberStyles style = NumberStyles.Number;
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-EN");

                    // Get "Stanje podataka: " date
                    double oaDateValue = worksheet.Cells[12, 5].GetValue<double>();
                    dateTimeOfData = DateTime.FromOADate(oaDateValue);

                    StanUpdate stanUpdate = new()
                    {
                        UpdateBegan = DateTime.Now,
                        ExecutedBy = userName,
                    };

                    await _unitOfWork.StanUpdate.Add(stanUpdate);
                    _ = await _unitOfWork.Complete();

                    IEnumerable<Stan> allStanovi = await _unitOfWork.Stan.GetAll();
                    Dictionary<int, Stan> stanoviDictionary = allStanovi
                        .GroupBy(stan => stan.StanId)
                        .ToDictionary(g => g.Key, g => g.First());

                    List<Stan> stanoviZaUpdate = new();
                    List<Stan> stanoviZaAdd = new();

                    for (int row = 14; row <= rowCount; row++)
                    {
                        try
                        {
                            int stanId = int.Parse(worksheet.Cells[row, 3].Text);

                            if (stanoviDictionary.TryGetValue(stanId, out Stan stan))
                            {
                                stan.SifraObjekta = int.Parse(worksheet.Cells[row, 4].Text);
                                stan.Vrsta = worksheet.Cells[row, 5].Text;
                                stan.Adresa = worksheet.Cells[row, 6].Text;
                                stan.Kat = worksheet.Cells[row, 7].Text;
                                stan.BrojSTana = worksheet.Cells[row, 8].Text;
                                stan.Naselje = worksheet.Cells[row, 9].Text;
                                stan.Četvrt = worksheet.Cells[row, 10].Text;
                                stan.Površina = double.TryParse(worksheet.Cells[row, 11].Text, style, culture, out double d) ? d : null;
                                stan.StatusKorištenja = worksheet.Cells[row, 12].Text;
                                stan.Korisnik = worksheet.Cells[row, 13].Text;
                                stan.Vlasništvo = worksheet.Cells[row, 14].Text;
                                stan.DioNekretnine = worksheet.Cells[row, 15].Text;
                                stan.Sektor = worksheet.Cells[row, 16].Text;
                                stan.Status = worksheet.Cells[row, 17].Text;
                                stanoviZaUpdate.Add(stan);
                            }
                            else
                            {
                                Stan newStan = new()
                                {
                                    SifraObjekta = int.Parse(worksheet.Cells[row, 4].Text),
                                    Vrsta = worksheet.Cells[row, 5].Text,
                                    Adresa = worksheet.Cells[row, 6].Text,
                                    Kat = worksheet.Cells[row, 7].Text,
                                    BrojSTana = worksheet.Cells[row, 8].Text,
                                    Naselje = worksheet.Cells[row, 9].Text,
                                    Četvrt = worksheet.Cells[row, 10].Text,
                                    Površina = double.TryParse(worksheet.Cells[row, 11].Text, style, culture, out double d) ? d : null,
                                    StatusKorištenja = worksheet.Cells[row, 12].Text,
                                    Korisnik = worksheet.Cells[row, 13].Text,
                                    Vlasništvo = worksheet.Cells[row, 14].Text,
                                    DioNekretnine = worksheet.Cells[row, 15].Text,
                                    Sektor = worksheet.Cells[row, 16].Text,
                                    Status = worksheet.Cells[row, 17].Text,
                                };

                                stanoviZaAdd.Add(newStan);
                            }

                        }
                        catch (Exception e)
                        {
                            _logger.Error(loggerTemplate + e.Message);

                            stanUpdate.Interrupted = true;
                            _ = await _unitOfWork.StanUpdate.Update(stanUpdate);
                        }
                    }

                    if (stanoviZaUpdate.Count != 0)
                    {
                        await _unitOfWork.Stan.UpdateRange(stanoviZaUpdate);
                    }

                    if (stanoviZaAdd.Count != 0)
                    {
                        await _unitOfWork.Stan.AddRange(stanoviZaAdd);
                    }

                    _ = await _unitOfWork.Complete();

                    stanUpdate.UpdateEnded = DateTime.Now;
                    stanUpdate.UpdateComplete = true;
                    stanUpdate.DateOfData = dateTimeOfData;
                    _ = await _unitOfWork.StanUpdate.Update(stanUpdate);
                }
                catch (Exception e)
                {
                    _logger.Error(loggerTemplate + e.Message);

                    return new JsonResult(new
                    {
                        success = false,
                        message = e.Message
                    });
                }
            }

            string message = "Uspješno uploadano";
            _logger.Information(loggerTemplate + message);
            return new JsonResult(new
            {
                success = true,
                message
            });
        }
    }
}
