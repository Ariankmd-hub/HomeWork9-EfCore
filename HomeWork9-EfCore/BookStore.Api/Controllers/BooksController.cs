using BookStore.Application;
using BookStore.Application.DTOs;
using BookStore.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IValidator<CreateBookRequest> _createBookValidator;
        private readonly IValidator<UpdateBookRequest> _updateBookValidator;

        public BooksController(
            IBookService bookService,
            IValidator<CreateBookRequest> createBookValidator,
            IValidator<UpdateBookRequest> updateBookValidator)
        {
            _bookService = bookService;
            _createBookValidator = createBookValidator;
            _updateBookValidator = updateBookValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBookRequest request)
        {
            var validationResult =
                await _createBookValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    Field = error.PropertyName,
                    Message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                Description = request.Description,
                ISBN = request.ISBN,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            await _bookService.AddAsync(book);

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
         int id,[FromBody] UpdateBookRequest request)
        {
            var validationResult =
                await _updateBookValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => new
                {
                    Field = error.PropertyName,
                    Message = error.ErrorMessage
                });

                return BadRequest(errors);
            }

            var result = await _bookService.UpdateAsync(id, request);

            return result ? NoContent() : NotFound();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            return result ? Ok() : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            return book is null ? NotFound() : Ok(book);
        }

        [HttpGet("titles")]
        public async Task<IActionResult> Titles()
        {
            var titles = await _bookService.GetTitlesAsync();

            return Ok(titles);
        }

        [HttpGet]
        public async Task<IActionResult> GetPage(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5)
        {
            var books = await _bookService.GetPageAsync(page, pageSize);

            return Ok(books);
        }
    }
}
