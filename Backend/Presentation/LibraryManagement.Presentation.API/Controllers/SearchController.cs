using LibraryManagement.Core.Application.Features.Search.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMediator mediator;
        public SearchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await mediator.Send(new SearchQueryRequest { Keyword = keyword });
            return Ok(result);
        }
    }
}
