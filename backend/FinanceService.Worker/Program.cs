using FinanceService;
using FinanceService.Domain;
using FinanceService.Infrastructure;
using FinanceService.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.Configure<SmtpEmailSettings>(builder.Configuration.GetSection("SmtpEmailSettings"));
builder.Services.AddSingleton<IEmailSender>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<SmtpEmailSettings>>().Value;
    return new SmtpEmailSender(settings);
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("FinanceService.WebApi")));

builder.Services.AddScoped<IContributionRepository, ContributionRepository>();
builder.Services.AddScoped<IContributionSettingsRepository, ContributionSettingsRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IContributionService, ContributionService>();
builder.Services.AddScoped<IContributionSettingsService, ContributionSettingsService>();
builder.Services.AddScoped<IPersonService, PersonService>();


var host = builder.Build();
host.Run();