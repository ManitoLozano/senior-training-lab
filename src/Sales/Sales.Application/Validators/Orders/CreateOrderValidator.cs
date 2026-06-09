using FluentValidation;
using Sales.Application.Models.Orders;

namespace Sales.Application.Validators.Orders;

public class CreateOrderValidator: AbstractValidator<CreateOrderInput>
{
    public CreateOrderValidator()
    {
        RuleFor(order => order.TotalAmount)
            .GreaterThan(0)
            .WithMessage("Total amount cannot be less than zero");
    }
}