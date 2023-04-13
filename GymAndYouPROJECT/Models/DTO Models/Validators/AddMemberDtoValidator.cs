using FluentValidation;

namespace GymAndYou.DTO_Models.Validators
{
    public class AddMemberDtoValidator : AbstractValidator<UpsertMemberDTO>
    {
        private string[] Sex = new string[]{"male","female"};
        public AddMemberDtoValidator()
        {
            RuleFor( p => p.Pesel)
                .NotNull()
                .NotEmpty()
                .Matches("^[0-9]{2}([02468]1|[13579][012])(0[1-9]|1[0-9]|2[0-9]|3[01])[0-9]{5}$")
                .WithMessage("This PESEL is incorrect please change it");

            RuleFor( p => p.Sex)
                .NotNull()
                .NotEmpty();


            RuleFor( p => p.FirstName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor( p => p.LastName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor( p => p.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor( p => p.FirstName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor( p => p.Phone)
                .NotEmpty()
                .NotNull()
                .Matches("(?<!\\w)(\\(?(\\+|00)?48\\)?)?[ -]?\\d{3}[ -]?\\d{3}[ -]?\\d{3}(?!\\w)");

            RuleFor( p => p.BirthDay)
                .NotEmpty()
                .NotNull();

            RuleFor( p => p.Sex)
                .Custom((value,context) => 
                {
                    if(value is not null && !Sex.Contains(value.ToLower()))
                    {
                        context.AddFailure("Sex",$"""Sex should be in [ {String.Join(" or ", Sex) } ]""");
                    }
                });
                 
        }
    }
}
