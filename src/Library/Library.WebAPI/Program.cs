using Library.Application.Mappers;
using Library.Application.Options;
using Library.Application.Services;
using Library.Application.Services.Interfaces;
using Library.Domain.Repositories;
using Library.Infrastructure;
using Library.Infrastructure.ExternalServices;
using Library.Infrastructure.Repositories;
using Library.WebAPI;
using Library.WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BookProfile).Assembly);

string DB_HOST = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
string DB_NAME = Environment.GetEnvironmentVariable("DB_NAME") ?? "Library";
string DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Passw0rd!";
//builder.Services.AddDbContext<LibraryContext>(options =>
//    options.UseSqlServer($"Server={DB_HOST};Database={DB_NAME};User Id=SA;Password={DB_PASSWORD};"));
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IImageDeleteService, CloudinaryImageDeleteService>();
builder.Services.AddScoped<IImageUploadService, CloudinaryImageUploadService>();

builder.Services.Configure<CloudinaryOptions>(
    builder.Configuration.GetSection(nameof(CloudinaryOptions)));

var app = builder.Build();

CreateAndPopulateDB.CreateDB(app);
CreateAndPopulateDB.PopulateDB(app);

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseMiddleware(typeof(ExcpetionHandlerMiddleware));
app.MapControllers();

app.Run();

// Added for integration tests
public partial class Program { }