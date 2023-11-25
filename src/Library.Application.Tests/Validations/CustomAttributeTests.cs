using Library.Application.Validations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Tests.Validations
{
    public class CustomAttributeTests
    {
        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData("AVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLong" +
            "FirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstName" +
            "AVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstName")]
        [InlineData("A AVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLong" +
            "FirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstName" +
            "AVeryLongFirstNameAVAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFi" +
            "AVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNamerstNameery" +
            "LongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstNameAVeryLongFirstName")]
        public void ReturnErrorWhenValueIsInvalid(string value)
        {
            var validationContext = new ValidationContext(value);
            var attribute = new AuthorNameValidation();

            var result = attribute.GetValidationResult(value, validationContext);

            Assert.NotNull(result);
            Assert.NotEmpty(result.ErrorMessage);
        }

        [Theory]
        [InlineData("Vinicius")]
        [InlineData("Vinicius França de Oliveira")]
        public void ReturnSuccessWhenValueIsValid(string value)
        {
            var validationContext = new ValidationContext(value);
            var attribute = new AuthorNameValidation();

            var result = attribute.GetValidationResult(value, validationContext);

            Assert.Equal(ValidationResult.Success, result);
        }
    }
}
