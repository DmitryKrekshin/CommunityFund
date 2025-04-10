using FinanceService.Domain;

namespace FinanceService.WebApi.Report;

public interface IContributionReportGenerator
{
    byte[] Generate(List<(PersonEntity person, List<ContributionEntity> contributions)> data);
}