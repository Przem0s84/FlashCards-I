using FlashCards.Entities;
using FluentValidation;

namespace FlashCards_I.Models.Validators
{
    public class RegisterUDtoValidator : AbstractValidator<RegistrationUDto>
    {
       
        public RegisterUDtoValidator(FlashCardsDbContext dbContext)
        {
            RuleFor(p=>p.Email).NotEmpty()
                .EmailAddress();
            RuleFor(p => p.Password).MinimumLength(6);
            RuleFor(p => p.ConfirmPassword).Equal(e => e.Password);

            RuleFor(p => p.Email).Custom((value, context) =>
            {
                var isuse = dbContext.Users.Any(u=>u.Email==value);
                if(isuse)
                {
                    context.AddFailure("Email", "Email already in use");
                }

            });
        }
    }
}
