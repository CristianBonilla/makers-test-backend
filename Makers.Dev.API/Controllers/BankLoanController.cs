using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Makers.Dev.API.Filters;
using Makers.Dev.Contracts.Services;
using Makers.Dev.Contracts.DTO.BankLoan;
using Makers.Dev.Domain.Entities;

namespace Makers.Dev.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ServiceErrorExceptionFilter]
public class BankLoanController(IMapper mapper, IBankLoanService bankLoanService) : ControllerBase
{
  readonly IMapper _mapper = mapper;
  readonly IBankLoanService _bankLoanService = bankLoanService;

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BankLoanResponse))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> AddBankLoan([FromBody] BankLoanRequest bankLoanRequest)
  {
    BankLoanEntity bankLoan = _mapper.Map<BankLoanEntity>(bankLoanRequest);
    BankLoanEntity addedbankLoan = await _bankLoanService.AddBankLoan(bankLoan);
    BankLoanResponse bankLoanResponse = _mapper.Map<BankLoanResponse>(addedbankLoan);

    return CreatedAtAction(nameof(AddBankLoan), bankLoanResponse);
  }

  [HttpPut("approve/{superUserId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankLoanResponse))]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> ApproveBankLoan(Guid superUserId, [FromQuery] Guid userId, [FromQuery] Guid bankLoanId)
  {
    BankLoanEntity bankLoan = await _bankLoanService.ApproveBankLoan(superUserId, userId, bankLoanId);
    BankLoanResponse bankLoanResponse = _mapper.Map<BankLoanResponse>(bankLoan);

    return Ok(bankLoanResponse);
  }

  [HttpPut("reject/{superUserId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankLoanResponse))]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> RejectBankLoan(Guid superUserId, [FromQuery] Guid userId, [FromQuery] Guid bankLoanId)
  {
    BankLoanEntity bankLoan = await _bankLoanService.RejectBankLoan(superUserId, userId, bankLoanId);
    BankLoanResponse bankLoanResponse = _mapper.Map<BankLoanResponse>(bankLoan);

    return Ok(bankLoanResponse);
  }

  [HttpDelete("{userId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankLoanResponse))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> DeleteBankLoanById(Guid userId, [FromQuery] Guid bankLoanId)
  {
    BankLoanEntity bankLoan = await _bankLoanService.DeleteBankLoanById(userId, bankLoanId);
    BankLoanResponse bankLoanResponse = _mapper.Map<BankLoanResponse>(bankLoan);

    return Ok(bankLoanResponse);
  }

  [HttpGet("{userId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankLoansResult))]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IActionResult GetBankLoansByUserId(Guid userId)
  {
    var bankLoans = _bankLoanService.GetBankLoansByUserId(userId);

    return Ok(_mapper.Map<BankLoansResult>(bankLoans));
  }

  [HttpGet("pending/{superUserId}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IAsyncEnumerable<BankLoansResult>))]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public IAsyncEnumerable<BankLoansResult> GetPendingBankLoans(Guid superUserId)
  {
    var bankLoans = _bankLoanService.GetPendingBankLoans(superUserId)
      .Select(bankLoan => _mapper.Map<BankLoansResult>(bankLoan));

    return bankLoans;
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BankLoanResponse))]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<IActionResult> FindBankLoanById([FromQuery] Guid userId, [FromQuery] Guid bankLoanId)
  {
    BankLoanEntity bankLoan = await _bankLoanService.FindBankLoanById(userId, bankLoanId);

    return Ok(_mapper.Map<BankLoanResponse>(bankLoan));
  }
}
