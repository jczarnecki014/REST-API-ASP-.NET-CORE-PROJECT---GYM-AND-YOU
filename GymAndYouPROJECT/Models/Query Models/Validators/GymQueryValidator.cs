using FluentValidation;
using GymAndYou.StaticData;

namespace GymAndYou.DTO_Models.Validators
{
    public class GymQueryValidator:AbstractValidator<GymQuery>
    {
        public GymQueryValidator()
        {
            RuleFor(u => u.SortBy)
            .Must(value => !String.IsNullOrEmpty(value) && Static.SortByAllowedColumns.Contains(value))
            .WithMessage($"""SortBy must be in [{String.Join(',',Static.SortByAllowedColumns)}] """);

            RuleFor(u => u.PageSize)
                .Custom((value,context) => 
                {
                    if(!Static.AviablePageSizes.Contains(value))
                    {
                        context.AddFailure($"""PageSize must in {String.Join(',',Static.AviablePageSizes)}""");
                    }
                });

            RuleFor(u => u.PageNumber)
                .GreaterThan(Static.MimimumPageNumber);
        }
    }
}
