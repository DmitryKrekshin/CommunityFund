using FinanceService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.WebApi
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ContributionController(IContributionService contributionService) : ControllerBase
    {
        private const string UnhandledExceptionMessage = "Unhandled exception";

        [HttpGet]
        public async Task<IActionResult> GetContributions(CancellationToken cancellationToken = default)
        {
            try
            {
                var contributions =
                    await contributionService.GetAsync(contribution => true, cancellationToken);

                return Ok(contributions.ToList());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContribution([FromBody] ContributionModel model,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newContribution = new ContributionEntity
                {
                    PayerGuid = model.PayerGuid,
                    Amount = model.Amount,
                    Date = model.Date
                };

                var addedContribution = await contributionService.AddAsync(newContribution, cancellationToken);
                return Ok(addedContribution);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContribution(Guid contributionGuid, [FromBody] ContributionModel model,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var contribution = new ContributionEntity
                {
                    Guid = contributionGuid,
                    PayerGuid = model.PayerGuid,
                    Amount = model.Amount,
                    Date = model.Date
                };

                var result = await contributionService.UpdateAsync(contribution, cancellationToken);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContribution(Guid contributionGuid,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await contributionService.DeleteAsync(contributionGuid, cancellationToken);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }
    }
}