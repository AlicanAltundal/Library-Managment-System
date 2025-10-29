using LibraryManagement.Core.Application.Bases;
using LibraryManagement.Core.Application.Interfaces.AutoMapper;
using LibraryManagement.Core.Application.Interfaces.UnitOfWorks;
using LibraryManagement.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Application.Features.Books.Command.CreateBook
{
    public class CreateBookCommandHandler : BaseHandler, IRequestHandler<CreateBookCommandRequest, Unit>
    {
        public CreateBookCommandHandler(MapperInt mapperInt, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
            : base(mapperInt, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateBookCommandRequest request, CancellationToken cancellationToken)
        {
            // 1️⃣ Kitap nesnesi oluştur
            var book = new Book
            {
                ISBN = request.ISBN,
                Title = request.Title,
                Genre = request.Genre,
                PublicationDate = request.PublicationDate,
                PublisherId = request.PublisherId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                
            };

            // 2️⃣ Kitabı ekle ve kaydet -> ID oluşturulsun
            await unitOfWork.GetWriteRepository<Book>().AddAsync(book);
            await unitOfWork.SaveAsync();

            // 3️⃣ Yazar ID’leri varsa BookAuthor kayıtlarını oluştur
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
