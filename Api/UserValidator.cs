using BlazorSwa.Shared;
using FluentValidation;

namespace Api
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("Preencha o campo 'Email'")
                .EmailAddress().WithMessage("Preencha o campo 'Email' com um email válido");

            RuleFor(c => c.Name).NotEmpty().WithMessage("Preencha o campo 'Nome'")
                .MinimumLength(3).WithMessage("O campo 'Nome' deve ter ao menos 3 caracteres")
                .MaximumLength(100).WithMessage("O campo 'Nome' deve ter no máximo 100 caracteres");
        }
    }
}