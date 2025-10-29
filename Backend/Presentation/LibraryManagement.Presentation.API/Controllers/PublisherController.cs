using LibraryManagement.Core.Application.Features.Books.Command.CreateBook;
using LibraryManagement.Core.Application.Features.Books.Command.DeleteBook;
using LibraryManagement.Core.Application.Features.Books.Command.UpdateBook;
using LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks;
using LibraryManagement.Core.Application.Features.Books.Queries.GetBookById;
using LibraryManagement.Core.Application.Features.Publishers.Command.CreatePublisher;
using LibraryManagement.Core.Application.Features.Publishers.Command.DeletePublisher;
using LibraryManagement.Core.Application.Features.Publishers.Command.UpdatePublisher;
using LibraryManagement.Core.Application.Features.Publishers.Queries.GetAllPublishers;
using LibraryManagement.Core.Application.Features.Publishers.Queries.GetBooksByPublisher;
using LibraryManagement.Core.Application.Features.Publishers.Queries.GetPublisherById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IMediator mediator;
        public PublisherController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            var response = await mediator.Send(new GetAllPublishersQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(CreatePublisherCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] UpdatePublisherCommandRequest request)
        {
            request.Id = id; // path'ten gelen id'yi modele yaz
            await mediator.Send(request);
            return Ok(new { message = "Yayınevi güncel/lendi." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            await mediator.Send(new DeletePublisherCommandRequest { Id = id });
            return Ok(new { message = "Yayınevi başarıyla silindi." });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetPublisherByIdQueryRequest { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooksByPublisher(int id)
        {
            var result = await mediator.Send(new GetBooksByPublisherQueryRequest { Id = id });
            return Ok(result);
        }

    }
}
