using FinanceService.Domain;
using FinanceService.Infrastructure;
using FinanceService;
using FinanceService.WebApi;
using FinanceService.WebApi.Report;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("FinanceService.WebApi")));

// Repositories
builder.Services.AddScoped<IContributionRepository, ContributionRepository>();
builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IContributionSettingsRepository, ContributionSettingsRepository>();

// Services
builder.Services.AddScoped<IContributionService, ContributionService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IContributionSettingsService, ContributionSettingsService>();

builder.Services.AddScoped<IContributionReportGenerator, ContributionReportGenerator>();
QuestPDF.Settings.License = LicenseType.Community;

// todo удалить как починю AuthenticationBuilder AddAuth
// http client
builder.Services.AddHttpClient();

// todo вернуть как починю AuthenticationBuilder AddAuth
//builder.Services.AddAuth(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://192.168.0.120:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// todo переключить как починю AuthenticationBuilder AddAuth
app.UseMiddleware<AuthMiddleware>();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();