using FluentValidation;
using MT.Domain.Enums;
using MT.Infrastructure.Models;

namespace MerchTrade.Validators;

public class UserModelValidator : AbstractValidator<UserModel>
{
    public UserModelValidator()
    {
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(100);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        RuleFor(x => x.Contacts).NotNull().SetValidator(new ContactsModelValidator());
        RuleFor(x => x.Socials).NotNull().SetValidator(new SocialsModelValidator());
        RuleFor(x => x.Personals).NotNull().SetValidator(new PersonalsModelValidator());
    }
}

public class ContactsModelValidator : AbstractValidator<ContactsModel>
{
    public ContactsModelValidator()
    {
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
    }
}

public class SocialsModelValidator : AbstractValidator<SocialsModel>
{
    public SocialsModelValidator()
    {
        RuleForEach(x => new[] { x.Telegram, x.Vkontakte, x.Instagram, x.Twitter })
            .MaximumLength(200).When(x => x != null);
    }
}

public class PersonalsModelValidator : AbstractValidator<PersonalsModel>
{
    public PersonalsModelValidator()
    {
        RuleFor(x => x.FirstName).MaximumLength(100);
        RuleFor(x => x.LastName).MaximumLength(100);
    }
}
