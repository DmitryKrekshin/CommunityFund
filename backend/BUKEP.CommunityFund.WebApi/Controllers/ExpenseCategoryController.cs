using BUKEP.CommunityFund.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BUKEP.CommunityFund.WebApi;

[ApiController]
[Route("api/v1/[controller]")]
public class ExpenseCategoryController(IExpenseCategoryService expenseCategoryService) : ControllerBase
{
    private const string UnhandledExceptionMessage = "Unhandled exception";

    [HttpGet]
    public async Task<IActionResult> GetExpenseCategories(CancellationToken cancellationToken = default)
    {
        try
        {
            var expenseCategories =
                await expenseCategoryService.GetAsync(expenseCategory => true, cancellationToken);

            return Ok(expenseCategories.ToList());
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddExpenseCategory([FromBody] ExpenseCategoryModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newExpenseCategory = new ExpenseCategoryEntity
            {
                Name = model.Name
            };

            var addedExpenseCategory =
                await expenseCategoryService.AddAsync(newExpenseCategory, cancellationToken);
            return Ok(addedExpenseCategory);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateExpenseCategory(Guid expenseCategoryGuid,
        [FromBody] ExpenseCategoryModel model, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expenseCategory = new ExpenseCategoryEntity
            {
                Guid = expenseCategoryGuid,
                Name = model.Name
            };

            var result = await expenseCategoryService.UpdateAsync(expenseCategory, cancellationToken);
            return Ok(result);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExpenseCategory(Guid expenseCategoryGuid, CancellationToken cancellationToken = default)
    {
        try
        {
            await expenseCategoryService.DeleteAsync(expenseCategoryGuid, cancellationToken);
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }
}