using Api.Models.Dto.Account;
using FluentValidation;

namespace Api.Validation;

public class AccountValidation : AbstractValidator<AccountLoginRequestDto>
{
    public AccountValidation()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("El correo no puede estar vacio").EmailAddress().WithMessage("Tiene que ser un correo valido");

        RuleFor(x => x.Password).NotEmpty().WithMessage("La clave es requerida");
    }
}
