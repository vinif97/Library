using Library.Domain.ValueObjects;

namespace Library.Domain.Entities
{
    public class Book
    {
        public int BookId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public string? CoverUrl { get; private set; }
        public int ReleaseYear { get; private set; }
        public Author Author { get; private set; }

        public Book() { }

        public Book(string title, string authorName, int releaseYear, string? description = null, string? coverUrl = null) 
        {
            Title = title;
            Author = new Author(authorName);
            Description = description;
            CoverUrl = coverUrl;
            ReleaseYear = releaseYear;
        }

        public void SetBookId(int bookId) => BookId = bookId;
        public void SetAuthorName(string fullname) => Author = new Author(fullname);
        public void SetDescription(string description) => Description = description;
        public void SetTitle(string title) => Title = title;
        public void SetReleaseYear(int releaseYear) => ReleaseYear = releaseYear;
        public void SetCoverUrl(string coverUrl) => CoverUrl = coverUrl;
    }
}
