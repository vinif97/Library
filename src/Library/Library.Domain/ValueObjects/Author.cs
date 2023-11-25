using Library.Domain.Helpers.Extensions;
using System.Text;

namespace Library.Domain.ValueObjects
{
    public class Author
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName 
        { 
            get => FirstName + " " + Surname; 
        }

        public Author() { }

        public Author(string fullName)
        {
            (FirstName, Surname) = fullName.GetFirstNameAndSurnameFromFullName();
        }

        public Author(string firstName, string surname)
        { 
            FirstName = firstName;
            Surname = surname;
        }
    }
}
