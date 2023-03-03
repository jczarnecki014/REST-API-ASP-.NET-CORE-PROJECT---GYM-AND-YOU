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
                .NotEmpty()
                .Custom((value,context) => 
                {
                    if(!Sex.Contains(value.ToLower()))
                    {
                        context.AddFailure("Sex",$"""Sex should be in [ {String.Join(" or ", Sex) } ]""");
                    }
                });
        }
    }
}
