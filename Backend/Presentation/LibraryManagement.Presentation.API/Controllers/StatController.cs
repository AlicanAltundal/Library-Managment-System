using LibraryManagement.Core.Application.Features.Stats.Queries.GetDashboardData;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        private readonly IMediator mediator;

        public StatController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            var result = await mediator.Send(new GetDashboardDataQueryRequest());
            return Ok(result);
        }
    }
}
