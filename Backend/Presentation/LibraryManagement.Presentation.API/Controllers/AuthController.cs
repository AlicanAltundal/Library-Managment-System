using LibraryManagement.Core.Application.Features.Auth.Command.LoginMember;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginMemberCommandRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }
    }
}
