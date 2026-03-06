using FluentValidation;
using MT.Infrastructure.Models;

namespace MerchTrade.Validators;

public class ItemModelValidator : AbstractValidator<ItemModel>
{
    public ItemModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required.")
            .MaximumLength(500);
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).When(x => x.Price.HasValue);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).When(x => x.Quantity.HasValue);
    }
}
