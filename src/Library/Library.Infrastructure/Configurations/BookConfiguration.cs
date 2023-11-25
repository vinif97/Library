using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.BookId);

            builder.Property(book => book.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(book => book.Description)
                .IsRequired(false);

            builder.Property(book => book.CoverUrl)
                .IsRequired(false);

            builder.Ignore(book => book.Author);

            builder.OwnsOne(book => book.Author, np =>
            {
                np.Property(author => author.FirstName)
                    .HasMaxLength(64)
                    .HasColumnName("FirstName")
                    .IsRequired(false);
                np.Property(author => author.Surname)
                    .HasMaxLength(512)
                    .HasColumnName("Surname")
                    .IsRequired(false);
            });
        }
    }
}
