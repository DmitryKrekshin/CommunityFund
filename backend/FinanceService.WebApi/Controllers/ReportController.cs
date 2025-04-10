using FinanceService.Domain;
using FinanceService.WebApi.Report;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.WebApi;

[ApiController]
[Route("api/v1/[controller]")]
public class ReportController(IPersonService personService, IContributionService contributionService, IContributionReportGenerator contributionReportGenerator) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetFullReport()
    {
        var people = await personService.GetAsync(x => true);
        var data = new List<(PersonEntity, List<ContributionEntity>)>();

        foreach (var person in people)
        {
            var contributions = await contributionService.GetAsync(c => c.PayerGuid == person.Guid, HttpContext.RequestAborted);
            data.Add((person, contributions.ToList()));
        }

        var pdf = contributionReportGenerator.Generate(data);

        return File(pdf, "application/pdf", $"contributions_report_{DateTime.Now:yyyyMMdd}.pdf");
    }
}