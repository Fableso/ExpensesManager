using Core.DependencyInjection;

using EntityProviders.Postgres.DependencyInjection;

using EventPublishers.Reports.DependencyInjection;

using ExpenseManagement.TransactionEntities.Configuration;
using ExpenseManagement.TransactionEntities.DependencyInjection;
using ExpenseManagement.TransactionEntities.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddSystemsManager($"/{ConfigurationKeys.SystemName}/");
var connectionString = builder.Configuration[ConfigurationKeys.PostgreSqlConnectionString];
var securityKey = builder.Configuration[ConfigurationKeys.SecurityKey];
var regionEndpoint = builder.Configuration[ConfigurationKeys.RegionEndpoint];
var queueUrl = builder.Configuration[ConfigurationKeys.QueueUrl];
builder.Services.AddPostgresEntityProviders(connectionString);
builder.Services.AddCoreServices();
builder.Services.AddAuthenticationServices(securityKey);
builder.Services.AddReportsEventPublishers(regionEndpoint, queueUrl);

builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();