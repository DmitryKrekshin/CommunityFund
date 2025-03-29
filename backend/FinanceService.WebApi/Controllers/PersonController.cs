using FinanceService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FinanceService.WebApi;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonController(IPersonService personService) : ControllerBase
{
    private const string UnhandledExceptionMessage = "Unhandled exception";

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        try
        {
            var persons = await personService.GetAsync(person => true, cancellationToken);

            return Ok(persons.ToList());
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPerson([FromBody] PersonModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPerson = new PersonEntity
            {
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                IsExcluded = false
            };

            var addedPerson = await personService.AddAsync(newPerson, cancellationToken);
            return Ok(addedPerson);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson(Guid personGuid, [FromBody] PersonModel model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPerson = new PersonEntity
            {
                Guid = personGuid,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                IsExcluded = false
            };

            var result = await personService.UpdateAsync(updatedPerson, cancellationToken);
            return Ok(result);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }

    [HttpPost]
    [Route("api/v1/[controller]/[action]")]
    public async Task<IActionResult> ExpelPerson(Guid personGuid, CancellationToken cancellationToken = default)
    {
        try
        {
            await personService.ExpelAsync(personGuid, cancellationToken);
            return Ok();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, UnhandledExceptionMessage);
        }
    }
}