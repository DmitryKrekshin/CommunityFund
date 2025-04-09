using FinanceService.Domain;

namespace FinanceService.Worker;

public class Worker(IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            using var scope = serviceScopeFactory.CreateScope();
            var contributionService = scope.ServiceProvider.GetRequiredService<IContributionService>();
            var contributionSettingsService = scope.ServiceProvider.GetRequiredService<IContributionSettingsService>();
            var personService = scope.ServiceProvider.GetRequiredService<IPersonService>();
            var smtpEmailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

            var persons = (await personService.GetAsync(p => true, stoppingToken)).ToList();

            foreach (var person in persons)
            {
                // Уведомление об исключении
                if (person.IsExcluded)
                {
                    await smtpEmailSender.SendEmailAsync(person.Email, "Исключение из пайщиков",
                        $"Уважаемый {person.Surname} {person.Name}, вы были исключены из пайщиков.");
                    continue;
                }

                var settings = (await contributionSettingsService
                        .GetAsync(s => s.PersonGuid == null || s.PersonGuid == person.Guid, stoppingToken))
                    .OrderByDescending(s => s.PersonGuid != null)
                    .FirstOrDefault();

                if (settings == null) continue;

                // Определим дату следующего ожидаемого платежа
                var now = DateTime.UtcNow.Date;
                var nextPaymentDate = new DateTime(now.Year, now.Month, settings.DateFrom.Day);

                if (nextPaymentDate < now)
                    nextPaymentDate = nextPaymentDate.AddMonths(1);

                var contributions = (await contributionService
                    .GetAsync(c => c.PayerGuid == person.Guid && c.Date >= new DateTime(now.Year, now.Month, 1).ToUniversalTime(),
                        stoppingToken)).ToList();

                var hasPaid = contributions.Any(c => c.Amount >= settings.Amount);

                if (!hasPaid)
                {
                    var daysUntilDue = (nextPaymentDate - now).Days;

                    if (daysUntilDue <= 5 && daysUntilDue >= 0)
                    {
                        await smtpEmailSender.SendEmailAsync(person.Email, "Скоро срок оплаты!",
                            $"Уважаемый {person.Surname}, ваш очередной взнос в размере {settings.Amount} руб. должен быть внесен до {nextPaymentDate:dd.MM.yyyy}.");
                    }
                    else if (daysUntilDue < 0)
                    {
                        await smtpEmailSender.SendEmailAsync(person.Email, "Просрочка оплаты",
                            $"Уважаемый {person.Surname}, вы не внесли взнос в размере {settings.Amount} руб., срок оплаты истёк {nextPaymentDate:dd.MM.yyyy}.");
                    }
                }
            }

            await Task.Delay(43200000, stoppingToken); // 12 часов
        }
    }
}