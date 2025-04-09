using FinanceService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.WebApi;

[ApiController]
[Route("api/v1/[controller]")]
public class ContributionSettingsController(IContributionSettingsService contributionSettingsService) : ControllerBase
{
    private const string UnhandledExceptionMessage = "Unhandled exception";

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Common(CancellationToken cancellationToken = default)
    {
        try
        {
            var settings = await contributionSettingsService.GetAsync(s => s.PersonGuid == null, cancellationToken);

            return Ok(settings.OrderByDescending(o => o.DateFrom).ToList());
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> Personal(CancellationToken cancellationToken = default)
    {
        try
        {
            var settings = await contributionSettingsService.GetAsync(s => s.PersonGuid != null, cancellationToken);

            return Ok(settings.OrderByDescending(o => o.DateFrom).ToList());
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ContributionSettings settings,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contributionSettingsEntity = new ContributionSettingsEntity
            {
                Amount = settings.Amount,
                PersonGuid = settings.PersonGuid,
                DateFrom = settings.DateFrom,
                DateTo = settings.DateTo
            };

            var addedContributionSettings =
                await contributionSettingsService.AddAsync(contributionSettingsEntity, cancellationToken);
            return Ok(addedContributionSettings);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ContributionSettings settings,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contributionSettingEntity = new ContributionSettingsEntity
            {
                Guid = settings.Guid,
                Amount = settings.Amount,
                PersonGuid = settings.PersonGuid,
                DateFrom = settings.DateFrom,
                DateTo = settings.DateTo
            };

            var result = await contributionSettingsService.UpdateAsync(contributionSettingEntity, cancellationToken);
            return Ok(result);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }
}