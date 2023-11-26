using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.Application.DTOs
{
    public class BookUpdateRequest
    {
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string? Title { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public string? AuthorName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [MaxLength(8000)]
        public string? Description { get; set; }
        public int? ReleaseYear { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}
