using Library.Domain.Entities;
using Library.Domain.Helpers.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.Validations.Attributes
{
    public class AuthorNameValidation : ValidationAttribute
    {
        private const int firstNameMaxLength = 64;
        private const int surnameMaxLength = 512;

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace((string?)value))
            {
                return new ValidationResult($"{validationContext.MemberName} cannot be null or empty");
            }

            var (firstname, surname) = value.ToString()!.GetFirstNameAndSurnameFromFullName();

            if (firstname.Length > firstNameMaxLength)
            {
                return new ValidationResult($"{nameof(Book.Author.FirstName)} cannot have more than {firstNameMaxLength} characters.");
            }
            if (surname.Length > surnameMaxLength)
            {
                return new ValidationResult($"{nameof(Book.Author.Surname)} cannot have more than {surnameMaxLength} characters.");
            }

            return ValidationResult.Success;
        }
    }
}
