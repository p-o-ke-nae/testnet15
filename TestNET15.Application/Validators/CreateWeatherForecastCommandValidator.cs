using FluentValidation;
using TestNET15.Application.UseCases.Commands;

namespace TestNET15.Application.Validators;

public class CreateWeatherForecastCommandValidator : AbstractValidator<CreateWeatherForecastCommand>
{
    public CreateWeatherForecastCommandValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.TemperatureC)
            .GreaterThanOrEqualTo(-50).WithMessage("Temperature must be at least -50°C.")
            .LessThanOrEqualTo(60).WithMessage("Temperature must not exceed 60°C.");

        RuleFor(x => x.Summary)
            .MaximumLength(100).WithMessage("Summary must not exceed 100 characters.");
    }
}
