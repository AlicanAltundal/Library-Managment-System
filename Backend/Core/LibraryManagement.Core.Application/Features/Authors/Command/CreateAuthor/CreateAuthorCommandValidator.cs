using FluentValidation;

namespace LibraryManagement.Core.Application.Features.Authors.Command.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommandRequest>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Yazar adı boş olamaz.")
                .MaximumLength(50).WithMessage("Yazar adı 50 karakterden uzun olamaz.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Yazar soyadı boş olamaz.")
                .MaximumLength(50).WithMessage("Yazar soyadı 50 karakterden uzun olamaz.");
        }
    }
}
