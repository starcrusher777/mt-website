using FluentValidation;
using MT.Infrastructure.Models;

namespace MerchTrade.Validators;

public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
    {
        RuleFor(x => x.Contacts).NotNull().SetValidator(new ContactsModelValidator());
        RuleFor(x => x.Socials).NotNull().SetValidator(new SocialsModelValidator());
        RuleFor(x => x.Personals).NotNull().SetValidator(new PersonalsModelValidator());
    }
}
