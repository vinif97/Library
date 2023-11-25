using AutoMapper;
using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Domain.ValueObjects;

namespace Library.Application.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Book, BookResponse>()
                .ForMember(dst => dst.AuthorName,
                    opt => opt.MapFrom(src => src.Author.FullName)
                );
        }
    }
}
