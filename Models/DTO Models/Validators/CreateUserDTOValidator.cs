using FluentValidation;
using GymAndYou.DatabaseConnection;
using GymAndYou.StaticData;
using Microsoft.EntityFrameworkCore;

namespace GymAndYou.Models.DTO_Models.Validators
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator(DbConnection _db) 
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                .NotNull()
                .Custom( (value,context) => 
                {
                    if(_db.Users.Any(u=> u.Email == value))
                    {
                        context.AddFailure("Sorry, but account with that email already exist");
                    }
                });

            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("The UserName field musn't be empty.")
                .NotNull()
                .Custom( (value,context) => 
                {
                    if(_db.Users.Any(u=> u.UserName == value))
                    {
                        context.AddFailure("Sorry, but account with that username already exist");
                    }
                });

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .Matches(Static.StrongPasswordREGEX) //StrongPassword requirement
                .WithMessage("Password must have at least one uppercase letter, one lowercase letter, one digit, " +
                             "one special character and is at least eight characters long  ");

            RuleFor(u => u.ConfirmPassword)
                .Equal(u => u.Password)
                .WithMessage("Password and confirmPassword must be same");

        } 
    }
}
