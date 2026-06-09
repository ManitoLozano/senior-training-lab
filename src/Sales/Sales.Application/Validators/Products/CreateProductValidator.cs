using FluentValidation;
using Sales.Application.Models.Products;

namespace Sales.Application.Validators.Products;

public sealed class CreateProductValidator: AbstractValidator<CreateProductInput>
{
    public  CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .MaximumLength(150)
            .WithMessage("Product name cannot exceed 150 characters");
        
        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Product description is required")
            .MaximumLength(500)
            .WithMessage("Product description cannot exceed 500 characters");
        
        RuleFor(p => p.Price)
            .NotEmpty()
            .WithMessage("Product price is required")
            .GreaterThan(0)
            .WithMessage("Product price must be greater than 0");
        
        RuleFor(p => p.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product stock quantity cannot be negative");
    }
}