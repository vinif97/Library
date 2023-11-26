using Library.Application.Validations.Attributes;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs
{
    public class BookCreateRequest
    {
        [MaxLength(256)]
        [Required]
        public string Title { get; set; }
        [MaxLength(8000)]
        public string? Description { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [AuthorNameValidation]
        public string AuthorName { get; set; }
        public IFormFile? CoverImage { get; set; }

        public BookCreateRequest() { }

        public BookCreateRequest(string title, string? authorName, int releaseYear, string? description = null)
        {
            Title = title;
            Description = description;
            ReleaseYear = releaseYear;
            AuthorName = authorName ?? "";
        }
    }
}
