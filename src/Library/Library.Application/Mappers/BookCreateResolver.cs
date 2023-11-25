using AutoMapper;
using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappers
{
    public class BookCreateResolver : IValueResolver<BookCreateRequest, Book, string>
    {
        public string Resolve(BookCreateRequest source, Book destination, string destMember, ResolutionContext context)
        {
            return source.AuthorName;
        }
    }
}
