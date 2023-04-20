using FluentValidation.TestHelper;
using GymAndYou.DTO_Models.Validators;
using GymAndYou.Entities;

namespace GymAndYouTESTS.ValidatorsTests
{
    public class AddEquipmentDTOValidator_TESTS:IClassFixture<AddEquipmentDTOValidator>
    {
        private readonly AddEquipmentDTOValidator _validator;

        public AddEquipmentDTOValidator_TESTS(AddEquipmentDTOValidator validator)
        {
            _validator = validator;
        }

        public static IEnumerable<object[]> GetInValidModels()
        {
            yield return new object[]
            {
                //Invalid Name - more thant 100 characters
                new UpsertEquipmentDTO()
                {
                    Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed id interdum lorem, vel rhoncus risus. " +
                                    "Curabitur felis sem, feugiat in molestie ut, elementum et elit. Suspendisse potenti. " +
                                    "Cras dapibus, nibh eget euismod scelerisque, mi nisl dictum lectus, id tincidunt felis " +
                                    "ipsum at odio. Morbi sollicitudin suscipit neque, in rhoncus nisi vehicula vel. Sed et " +
                                    "auctor ipsum. Proin eget diam ut est aliquet bibendum sed eget justo. Suspendisse" +
                                    " hendrerit pulvinar magna, a vulputate ex dignissim a.",
                    Description =   "Description",
                    MaxWeight = 100,
                    BodyPart="Legs"
                }
            };
            yield return new object[]
            {
                //Invalid Description - more thant 255 characters
                new UpsertEquipmentDTO()
                {
                    Name =   "Name",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed id interdum lorem, vel rhoncus risus. " +
                                    "Curabitur felis sem, feugiat in molestie ut, elementum et elit. Suspendisse potenti. " +
                                    "Cras dapibus, nibh eget euismod scelerisque, mi nisl dictum lectus, id tincidunt felis " +
                                    "ipsum at odio. Morbi sollicitudin suscipit neque, in rhoncus nisi vehicula vel. Sed et " +
                                    "auctor ipsum. Proin eget diam ut est aliquet bibendum sed eget justo. Suspendisse" +
                                    " hendrerit pulvinar magna, a vulputate ex dignissim a.",
                    MaxWeight = 100,
                    BodyPart="Legs"
                }
            };
            yield return new object[]
            {
                //Invalid MaxWeight - null
                new UpsertEquipmentDTO()
                {
                    Name =   "Name",
                    Description = "Description",
                    MaxWeight = 0,
                    BodyPart="Legs"
                }
            };
            yield return new object[]
            {
                //Invalid MaxWeight - null
                new UpsertEquipmentDTO()
                {
                    Name =   "Name",
                    Description = "Description",
                    MaxWeight = 0,
                    BodyPart="unnamed"
                }
            };
        }

        [Fact]
        public void Validation_ForValidModel_ReturnSuccess()
        {
            // arrange
                var model = new UpsertEquipmentDTO()
                {
                    BodyPart = "Legs",
                    Name = "Name",
                    Description = "Description",
                    MaxWeight = 100
                };
            // act
                var result = _validator.TestValidate(model);

            // assert
                result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInValidModels))]
        public void Validation_ForInValidModel_ReturnError(UpsertEquipmentDTO model)
        {
            // act 
                var result = _validator.TestValidate(model);

            // assert
                result.ShouldHaveAnyValidationError();
        }

    }
}
