using LibraryManagement.Core.Application.Features.Authors.Command.CreateAuthor;
using LibraryManagement.Core.Application.Features.Authors.Command.DeleteAuthor;

using LibraryManagement.Core.Application.Features.Authors.Command.UpdateAuthor;
using LibraryManagement.Core.Application.Features.Authors.Queries.GetAllAuthors;
using LibraryManagement.Core.Application.Features.Authors.Queries.GetAuthorById;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMediator mediator;

    public AuthorController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var result = await mediator.Send(new GetAllAuthorsQueryRequest());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetAuthorByIdQueryRequest { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorCommandRequest request)
    {
        await mediator.Send(request);
        return Ok("Yazar başarıyla oluşturuldu.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommandRequest request)
    {
        await mediator.Send(request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAuthor(DeleteAuthorCommandRequest request)
    {
        await mediator.Send(request);
        return Ok();
    }
}
