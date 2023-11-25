using Library.Domain.ValueObjects;

namespace Library.Application.DTOs
{
    public class BookResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
        public string AuthorName { get; set; }
        public int ReleaseYear { get; set; }
    }
}
