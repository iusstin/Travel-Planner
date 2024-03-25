using ApplicationCore.User.Commands.Register;
using Domain.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ApplicationCore.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCmd>
{
    public RegisterCommandValidator(IUserRepository userRepository)
    {
        RuleFor(c => c.model.Email).MustAsync(async (email, _) =>
        {
            return await userRepository.IsEmailUnique(email);
        }).WithMessage("Email already used");

        RuleFor(c => c.model.Email).Must((cmd, _) =>
        {
            string pattern = @"^([0-9a-zA-Z]" + @"([\+\-_\.][0-9a-zA-Z]+)*" + @")+" + @"(([0 - 9a - zA - Z][-\w] * [0 - 9a - zA - Z] *\.) + [a - zA - Z0 - 9]{ 2,17})$";
            Regex checkEmail = new Regex(pattern);
            return !checkEmail.IsMatch(cmd.model.Email);
        }).WithMessage("Email invalid");

        RuleFor(c => c.model.Email).NotEmpty().WithMessage("Email cannot be empty");

        RuleFor(u => u.Password).Must((model, _) =>
        {
            if (model.Password.Length > 8)
                return true;

            return false;
        }).WithMessage("Password invalid");
    }
}
