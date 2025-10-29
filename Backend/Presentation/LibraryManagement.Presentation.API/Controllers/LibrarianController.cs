using LibraryManagement.Core.Application.Features.Books.Command.CreateBook;
using LibraryManagement.Core.Application.Features.Books.Command.DeleteBook;
using LibraryManagement.Core.Application.Features.Books.Command.UpdateBook;
using LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks;
using LibraryManagement.Core.Application.Features.Librarians.Command.CreateLibrarian;
using LibraryManagement.Core.Application.Features.Librarians.Command.DeleteLibrarian;
using LibraryManagement.Core.Application.Features.Librarians.Command.UpdateLibrarian;
using LibraryManagement.Core.Application.Features.Librarians.Queries.GetAllLibrarians;
using LibraryManagement.Core.Application.Features.Librarians.Queries.GetLoanCountByLibrarian;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibrarianController : ControllerBase
    {
        private readonly IMediator mediator;
        public LibrarianController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibrarians()
        {
            var response = await mediator.Send(new GetAllLibrariansQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLibrarian(CreateLibrarianCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLibrarian(UpdateLibrarianCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteLibrarian(DeleteLibrarianCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanCount(int id)
        {
            var result = await mediator.Send(new GetLoanCountByLibrarianQueryRequest { Id = id });
            return Ok(result);
        }

    }
}
