using FluentValidation;
using Sales.Application.Models.Customers;
using Sales.Application.Validators.Common;

namespace Sales.Application.Validators.Customers;

public sealed class CreateCustomerInputValidator: AbstractValidator<CreateCustomerInput>
{
    public CreateCustomerInputValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(150)
            .WithMessage("Name cannot exceed 150");
        
        RuleFor(customer => customer.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is not valid");
        
        RuleFor(customer => customer.Document)
            .NotEmpty()
            .WithMessage("Document is required")
            .Must(DocumentValidator.IsValidCpfOrCnpj)
            .WithMessage("Document is not valid");
    }
}