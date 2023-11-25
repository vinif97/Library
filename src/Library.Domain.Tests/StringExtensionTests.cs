using Library.Domain.Helpers.Extensions;

namespace Library.Domain.Tests
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("Vinicius", "França de Oliveira", "Vinicius França de Oliveira")]
        [InlineData("Vinicius", "", "Vinicius")]
        [InlineData("Vinicius", "França", "     Vinicius França ")]
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