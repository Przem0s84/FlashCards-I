using FlashCards.Entities;
using FluentValidation;

namespace FlashCards_I.Models.Validators
{
    public class ResetPasswordDtoValidator:AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator(FlashCardsDbContext dbContext) 
        {
            RuleFor(p => p.Email).NotEmpty()
                    .EmailAddress();
            RuleFor(p => p.NewPassword).MinimumLength(6);
            RuleFor(p => p.RepeatNewPassword).Equal(e => e.NewPassword);

            RuleFor(p => p.Email).Custom((value, context) =>
            {
                var isuse = dbContext.Users.Any(u => u.Email == value);
                if (!isuse)
                {
                    context.AddFailure("User", "User not exist");
                }

            });

        }

    }
}
