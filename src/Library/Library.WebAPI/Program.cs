using AutoMapper;
using Library.Application.Mappers;
using Library.Application.Services;
using Library.Application.Services.Interfaces;
using Library.Domain.Repositories;
using Library.Infrastructure;
using Library.Infrastructure.Repositories;
using Library.WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(BookProfile).Assembly);
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseMiddleware(typeof(ExcpetionHandlerMiddleware));
app.MapControllers();

app.Run();

// Added for integration tests
public partial class Program { }