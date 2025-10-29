using LibraryManagement.Core.Application.Features.Books.Queries.GetAllBooks;
using LibraryManagement.Core.Application.Features.Books.Queries.GetBookById;
using LibraryManagement.Core.Application.Features.Loans.Command.CreateLoan;
using LibraryManagement.Core.Application.Features.Loans.Command.ReturnLoan;
using LibraryManagement.Core.Application.Features.Loans.Queries.GetAllLoans;
using LibraryManagement.Core.Application.Features.Loans.Queries.GetLoanById;
using LibraryManagement.Core.Application.Features.Loans.Queries.GetOverdueLoans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly IMediator mediator;

    public LoanController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLoans()
    {
        var response = await mediator.Send(new GetAllLoansQueryRequest());
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> CreateLoan(CreateLoanCommandRequest request)
    {
        try
        {
            await mediator.Send(request);
            return Ok(new { message = "Kitap ödünç verme işlemi başarıyla tamamlandı." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message }); // ✅ Artık 400 ve mesaj
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ReturnLoan(int id)
    {
        await mediator.Send(new ReturnLoanCommandRequest { LoanId = id });
        return Ok("Kitap iade edildi.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetLoanByIdQueryRequest { Id = id });
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet()]
    public async Task<IActionResult> GetOverdueLoans()
    {
        var result = await mediator.Send(new GetOverdueLoansQueryRequest());
        if (result == null || !result.Any())
            return NotFound("Geciken ödünç kaydı bulunamadı.");

        return Ok(result);
    }



}
