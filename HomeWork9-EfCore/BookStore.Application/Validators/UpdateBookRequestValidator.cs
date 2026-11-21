using BookStore.Application.DTOs;
using FluentValidation;

namespace BookStore.Application.Validators
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title can't be empty.")
                .MaximumLength(200)
                .WithMessage("Title can't be more than 200 characters.");

            RuleFor(x => x.Author)
                .NotEmpty()
                .WithMessage("Author can't be empty.")
                .MaximumLength(200)
                .WithMessage("Author can't be more than 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .WithMessage("Description can't be more than 2000 characters.");

            RuleFor(x => x.ISBN)
                .NotEmpty()
                .WithMessage("ISBN is required.")
                .MaximumLength(20)
                .WithMessage("ISBN can't be more than 20 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock quantity can't be negative.");
        }
    }
}
