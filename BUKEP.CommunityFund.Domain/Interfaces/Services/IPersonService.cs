using System.Linq.Expressions;

namespace BUKEP.CommunityFund.Domain;

public interface IPersonService
{
    PersonEntity AddPerson(PersonEntity person);
    
    PersonEntity EditPerson(PersonEntity person);

    void ExpelPerson(Guid personGuid);
    
    void ReadmitPerson(Guid personGuid);
    
    IEnumerable<PersonEntity> GetPersons(Expression<Func<PersonEntity, bool>> predicate);
}