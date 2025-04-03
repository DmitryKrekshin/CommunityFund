using System.Data;
using FinanceService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.WebApi;

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

            var addedExpenseCategory =
                await expenseCategoryService.AddAsync(model.Name, cancellationToken);
            return Ok(addedExpenseCategory);
        }
        catch(DuplicateNameException exception)
        {
            return StatusCode(StatusCodes.Status409Conflict, exception.Message);
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

            var result = await expenseCategoryService.UpdateAsync(expenseCategoryGuid, model.Name, cancellationToken);
            return Ok(result);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteExpenseCategory(Guid expenseCategoryGuid,
        CancellationToken cancellationToken = default)
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