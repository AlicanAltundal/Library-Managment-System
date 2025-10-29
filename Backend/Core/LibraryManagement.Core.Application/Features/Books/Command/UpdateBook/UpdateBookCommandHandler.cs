using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Command.UpdateBook
{
    public class UpdateBookCommandHandler : BaseHandler, IRequestHandler<UpdateBookCommandRequest, Unit>
    {
        public UpdateBookCommandHandler(
            MapperInt mapperInt,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor
        ) : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateBookCommandRequest request, CancellationToken cancellationToken)
        {
            var book = await unitOfWork.GetReadRepository<Book>()
                .GetAsync(
                    predicate: x => x.Id == request.Id && !x.IsDeleted
                );

            if (book == null)
                throw new Exception("Kitap bulunamadı.");

            // 2️⃣ Alanları güncelle
            book.ISBN = request.ISBN;
            book.Title = request.Title;
            book.Genre = request.Genre;
            book.PublicationDate = request.PublicationDate;
            book.PublisherId = request.PublisherId;

            // 3️⃣ Güncelle ve kaydet
            await unitOfWork.GetWriteRepository<Book>().UpdateAsync(book);
            await unitOfWork.SaveAsync();

            if (request.AuthorIds != null && request.AuthorIds.Any())
            {
                var bookAuthors = request.AuthorIds.Select(authorId => new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = authorId
                }).ToList();

                await unitOfWork.GetWriteRepository<BookAuthor>().AddRangeAsync(bookAuthors);
                await unitOfWork.SaveAsync();
            }

            return Unit.Value;

        }
    }
}
