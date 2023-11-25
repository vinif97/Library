﻿// <auto-generated />
using Library.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Library.Domain.Entities.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("CoverUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library.Domain.Entities.Book", b =>
                {
                    b.OwnsOne("Library.Domain.ValueObjects.Author", "Author", b1 =>
                        {
                            b1.Property<int>("BookId")
                                .HasColumnType("int");

                            b1.Property<string>("FirstName")
                                .HasMaxLength(64)
                                .HasColumnType("nvarchar(64)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("Surname")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Surname");

                            b1.HasKey("BookId");

                            b1.ToTable("Books");

                            b1.WithOwner()
                                .HasForeignKey("BookId");
                        });

                    b.Navigation("Author")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
