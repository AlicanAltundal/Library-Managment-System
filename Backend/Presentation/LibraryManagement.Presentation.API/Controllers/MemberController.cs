using LibraryManagement.Core.Application.Features.Books.Command.DeleteBook;
using LibraryManagement.Core.Application.Features.Books.Command.UpdateBook;
using LibraryManagement.Core.Application.Features.Members.Command.CreateMember;
using LibraryManagement.Core.Application.Features.Members.Command.DeleteMember;
using LibraryManagement.Core.Application.Features.Members.Command.UpdateMember;
using LibraryManagement.Core.Application.Features.Members.Queries.GetAllMembers;
using LibraryManagement.Core.Application.Features.Members.Queries.GetLoansByMember;
using LibraryManagement.Core.Application.Features.Members.Queries.GetMemberById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Presentation.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMediator mediator;
        public MemberController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var response = await mediator.Send(new GetAllMembersQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberCommandRequest request)
        {
            request.Id = id; // path'ten gelen id'yi modele yaz
            await mediator.Send(request);
            return Ok(new { message = "Üye güncel/lendi." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            await mediator.Send(new DeleteMemberCommandRequest { Id = id });
            return Ok(new { message = "Üye başarıyla silindi." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await mediator.Send(new GetMemberByIdQueryRequest { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoansByMember(int id)
        {
            var result = await mediator.Send(new GetLoansByMemberQueryRequest { MemberId = id });
            if (result == null || !result.Any()) return NotFound("Bu üyeye ait ödünç kaydı bulunamadı.");
            return Ok(result);
        }



    }
}
