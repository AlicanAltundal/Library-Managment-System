using LibraryManagement.Core.Application.Features.Books.Command.CreateBook;
using LibraryManagement.Core.Application.Features.Books.Command.DeleteBook;
using LibraryManagement.Core.Application.Features.Books.Command.UpdateBook;
using LibraryManagement.Core.Application.Features.Books.Queries.CheckIsbn;
using LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks;
using LibraryManagement.Core.Application.Features.Books.Queries.GetBookById;
using LibraryManagement.Core.Application.Features.Books.Queries.GetBooksByGenre;
using LibraryManagement.Core.Application.Features.Books.Queries.GetGenres;
using LibraryManagement.Core.Application.Features.Publishers.Command.DeletePublisher;
using LibraryManagement.Core.Application.Features.Publishers.Command.UpdatePublisher;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator mediator;
        public BookController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var response = await mediator.Send(new GetAllBooksQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommandRequest request)
        {
            request.Id = id; // path'ten gelen id'yi modele yaz
            await mediator.Send(request);
            return Ok(new { message = "Kitap güncel/lendi." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await mediator.Send(new DeleteBookCommandRequest { Id = id });
            return Ok(new { message = "Kitap başarıyla silindi." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetBookByIdQueryRequest { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{genre}")]
        public async Task<IActionResult> GetByGenre(string genre)
        {
            var result = await mediator.Send(new GetBooksByGenreQueryRequest { Genre = genre });
            if (result == null || !result.Any()) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var result = await mediator.Send(new GetGenresQueryRequest());
            return Ok(result);
        }

        [HttpGet("{isbn}")]
        public async Task<IActionResult> CheckIsbn(string isbn)
        {
            var available = await mediator.Send(new CheckISBNQueryRequest { ISBN = isbn });
            return Ok(new
            {
                isbn,
                isAvailable = available,
                message = available ? "ISBN kullanılabilir." : "Bu ISBN zaten mevcut."
            });
        }



    }
}