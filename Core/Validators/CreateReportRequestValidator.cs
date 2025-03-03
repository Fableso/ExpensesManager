using FluentValidation;

using PrimaryPorts.Models;

namespace Core.Validators;

public class CreateReportRequestValidator : AbstractValidator<ReportGenerationRequestData>
{
    public CreateReportRequestValidator()
    {
        RuleFor(x => x.From)
            .NotEmpty()
            .WithMessage("Start date is required");

        RuleFor(x => x.To)
            .NotEmpty()
            .WithMessage("End date is required");

        RuleFor(x => x.From)
            .LessThan(x => x.To)
            .WithMessage("Start date must be less than end date");
    }
}
