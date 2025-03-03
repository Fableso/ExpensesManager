using FluentValidation;

using PrimaryPorts.Models;

namespace Core.Validators;

public class CreateExpenseValidator : AbstractValidator<CreateExpenseData>
{
    public CreateExpenseValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required")
            .MaximumLength(50)
            .WithMessage("Category must be less than 50 characters");
        
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0");
        
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date is required")
            .GreaterThan(DateTime.MinValue)
            .WithMessage("Date must be greater than 01/01/0001");
    }
}
