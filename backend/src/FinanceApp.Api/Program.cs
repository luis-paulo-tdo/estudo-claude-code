using FinanceApp.Application.Insights;
using FinanceApp.Application.Insights.Rules;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=financeapp.db"));

builder.Services.AddScoped<IInsightRule, SpendingVariationRule>();
builder.Services.AddScoped<IInsightRule, CriticalBudgetRule>();
builder.Services.AddScoped<IInsightRule, PositiveBalanceSurplusRule>();
builder.Services.AddScoped<IInsightRule, Rule503020Rule>();
builder.Services.AddScoped<IInsightRule, NoEmergencyFundRule>();
builder.Services.AddScoped<InsightService>();

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p => p
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
