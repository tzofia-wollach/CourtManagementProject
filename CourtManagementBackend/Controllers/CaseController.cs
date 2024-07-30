using Microsoft.AspNetCore.Mvc;
using CourtManagementBackend.Models;
using CourtManagementBackend.Services;
using System.Threading.Tasks;

namespace CourtManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController : ControllerBase
    {
        private readonly CaseManagementService _caseManagementService;

        public CaseController(CaseManagementService caseManagementService)
        {
            _caseManagementService = caseManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase([FromBody] CaseData caseData)
        {
            var newCase = await _caseManagementService.CreateCaseAsync(caseData);
            return Ok(newCase);
        }
    }
}
