using FinanceService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.WebApi
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ExpenseController(IExpenseService expenseService) : ControllerBase
    {
        private const string UnhandledExceptionMessage = "Unhandled exception";

        [HttpGet]
        public async Task<IActionResult> GetExpenses(CancellationToken cancellationToken = default)
        {
            try
            {
                var expenses =
                    await expenseService.GetAsync(expenseCategory => true, cancellationToken);

                return Ok(expenses.ToList());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseModel model,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newExpense = new ExpenseEntity
                {
                    SpenderGuid = model.SpenderGuid,
                    Amount = model.Amount,
                    Comment = model.Comment,
                    Date = model.Date
                };

                var addedExpense = await expenseService.AddAsync(newExpense, cancellationToken);
                return Ok(addedExpense);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpenseCategory(Guid expenseGuid, [FromBody] ExpenseModel model,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var expense = new ExpenseEntity
                {
                    Guid = expenseGuid,
                    SpenderGuid = model.SpenderGuid,
                    Amount = model.Amount,
                    Comment = model.Comment,
                    Date = model.Date
                };

                var result = await expenseService.UpdateAsync(expense, cancellationToken);
                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteExpenseCategory(Guid expenseGuid,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await expenseService.DeleteAsync(expenseGuid, cancellationToken);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
            }
        }
    }
}