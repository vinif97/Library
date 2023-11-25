using Library.Domain.Helpers.Extensions;

namespace Library.Domain.Tests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("Vinicius", "Fran�a de Oliveira", "Vinicius Fran�a de Oliveira")]
        [InlineData("Vinicius", "", "Vinicius")]
        [InlineData("Vinicius", "Fran�a", "     Vinicius Fran�a ")]
        public void GetFirstNameAndSurnameCorrectlyFromFullName(string expectedFirstName, string expectedSurname, string fullName)
        {
            (string firstname, string surname) = fullName.GetFirstNameAndSurnameFromFullName();

            Assert.Equal(expectedFirstName, firstname);
            Assert.Equal(expectedSurname, surname);
        }

        [Fact]
        public void GetFirstNameAndSurnameFromFullNameThrowsExceptionWhenNameIsEmpty()
        {
            var fullName = "";

            void act() => fullName.GetFirstNameAndSurnameFromFullName();

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Name cannot be empty", exception.Message);
        }
    }
}