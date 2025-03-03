using Core.Abstractions;
using Core.Mapping;
using Core.Services;
using Core.Validators;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.Extensions.DependencyInjection;

using PrimaryPorts.Models;

namespace Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IExpensesService, ExpensesService>();
        services.AddScoped<IReportsService, ReportsService>();

        services.AddAutoMapper(config =>
        {
            config.AddProfile<MappingProfile>();
        });

        services.AddFluentValidationAutoValidation();
        services.AddTransient<IValidator<CreateExpenseData>, CreateExpenseValidator>();
        services.AddTransient<IValidator<ReportGenerationRequestData>, CreateReportRequestValidator>();
            
        return services;
    }
}
