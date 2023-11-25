using System.Text;

namespace Library.Domain.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static (string firstName, string surname) GetFirstNameAndSurnameFromFullName(this string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            fullName = fullName.Trim();
            int startSurnameIndex = fullName.IndexOf(' ') + 1;
            if (startSurnameIndex == 0)
            {
                return (fullName, string.Empty);
            }

            string firstName = fullName.Substring(0, startSurnameIndex - 1);
            string surname = fullName.Substring(startSurnameIndex);

            return (firstName, surname);
        }
    }
}
