using FluentValidation;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Command.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommandRequest>
    {
 
        private readonly IUnitOfWork _unitOfWork;
        

        public UpdateBookCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN zorunludur.")
                .Length(13).WithMessage("ISBN 13 haneli olmalıdır.")
                .MustAsync(NotExistISBN).WithMessage("Bu ISBN zaten kayıtlı.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Kitap adı zorunludur.")
                .MaximumLength(200);

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Tür alanı zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.PublicationDate)
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Yayın tarihi bugünden ileri olamaz.");

            RuleFor(x => x.PublisherId)
                .GreaterThan(0).WithMessage("Geçerli bir yayıncı seçiniz.");
        }

        private async Task<bool> NotExistISBN(string isbn, CancellationToken token)
        {
            var count = await _unitOfWork.GetReadRepository<Book>()
                .CountAsync(x => x.ISBN == isbn && !x.IsDeleted);

            return count == 0; // varsa 0'dan büyük olur, false döner
        }
    } 
    }
