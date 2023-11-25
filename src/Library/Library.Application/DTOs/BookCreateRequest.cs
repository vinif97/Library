using Library.Application.Validations.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs
{
    public class BookCreateRequest
    {
        [MaxLength(256)]
        [Required]
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public int ReleaseYear { get; private set; }
        [AuthorNameValidation]
        public string AuthorName { get; private set; }

        public BookCreateRequest(string title, string? authorName, int releaseYear, string? description = null)
        {
            Title = title;
            Description = description;
            ReleaseYear = releaseYear;
            AuthorName = authorName ?? "";
        }
    }
}
