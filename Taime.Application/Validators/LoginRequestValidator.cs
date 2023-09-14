using FluentValidation;
using Taime.Application.Contracts;
using Taime.Application.Enums;

namespace Taime.Application.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .Must(x => !string.IsNullOrEmpty(x))
                .EmailAddress()
                .WithErrorCode(TaimeApiErrors.TaimeApi_Post_400_Invalid_Login.ToString());

            RuleFor(x => x.Password)
                .Must(x => !string.IsNullOrEmpty(x))
                .WithErrorCode(TaimeApiErrors.TaimeApi_Post_400_Invalid_Login.ToString());
        }
    }
}
