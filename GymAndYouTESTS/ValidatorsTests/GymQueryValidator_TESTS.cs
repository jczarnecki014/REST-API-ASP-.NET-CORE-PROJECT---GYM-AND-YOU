using FluentAssertions;
using FluentValidation.TestHelper;
using GymAndYou.DTO_Models;
using GymAndYou.DTO_Models.Validators;
using GymAndYouTESTS.ValidatorsTests.TestCases;

namespace GymAndYouTESTS.ValidatorsTests
{
    public class GymQueryValidator_TESTS:IClassFixture<GymQueryValidator>
    {
        private readonly GymQueryValidator _validator;

        public GymQueryValidator_TESTS(GymQueryValidator validator)
        {
            _validator = validator;
        }

        [Theory]
        [ClassData(typeof(GymQueryValidator_TestCases_ValidModels))]
        public void Validation_ForValidQueryParameters_ReturnSuccess(GymQuery query)
        {
            
            // act
                var result = _validator.TestValidate(query);
            // assert
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [ClassData(typeof(GymQueryValidator_TestCases_InValidModels))]
        public void Validation_ForInValidQueryParameters_ReturnError(GymQuery query)
        {
            
            // act
                var result = _validator.TestValidate(query);
            // assert
                result.ShouldHaveAnyValidationError();
        }
    }
}
