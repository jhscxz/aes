using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace aes.Services.IServices
{
    public interface IOdsService
    {
#nullable enable
        Task<JsonResult> GetStanData(string? sid);
        Task<JsonResult> GetStanDataForOmm(string? OdsId);
    }
}