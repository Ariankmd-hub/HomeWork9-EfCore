using BookStore.Application;
using BookStore.Application.Services;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.Validators;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookRequestValidator>();

builder.Services.AddControllers();

builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
