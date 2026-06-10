using FluentValidation;
using Sales.Application.Models.Orders;

namespace Sales.Application.Validators.Orders;

public class CreateOrderValidator: AbstractValidator<CreateOrderInput>
{
    public CreateOrderValidator()
    {
        
    }
}