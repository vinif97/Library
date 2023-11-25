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
        public string? Description { get; set; }
        public int? ReleaseYear { get; set; }
    }
}
