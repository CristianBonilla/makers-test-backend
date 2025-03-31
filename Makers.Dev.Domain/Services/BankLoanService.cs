using Makers.Dev.Contracts.Services;
using Makers.Dev.Domain.Entities;
using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Domain.Helpers;
using Makers.Dev.Domain.Helpers.Exceptions;
using Makers.Dev.Infrastructure.Repositories.Auth.Interfaces;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.Domain.Services;

public class BankLoanService(
  IMakersDevRepositoryContext context,
  IBankLoanRepository bankLoanRepository,
  IUserRepository userRepository,
  IRoleRepository roleRepository) : IBankLoanService
{
  readonly IMakersDevRepositoryContext _context = context;
  readonly IBankLoanRepository _bankRepository = bankLoanRepository;
  readonly IUserRepository _userRepository = userRepository;
  readonly IRoleRepository _roleRepository = roleRepository;

  public async Task<BankLoanEntity> AddBankLoan(BankLoanEntity bankLoan)
  {
    CheckUserById(bankLoan.UserId);
    bankLoan.Status = BankLoanStatus.Pending;
    BankLoanEntity addedBankLoan = _bankRepository.Create(bankLoan);
    _ = await _context.SaveAsync();

    return addedBankLoan;
  }

  public Task<BankLoanEntity> ApproveBankLoan(Guid userId, Guid bankLoanId) => BankLoanStatusChange(userId, bankLoanId, BankLoanStatus.Approved);

  public Task<BankLoanEntity> RejectBankLoan(Guid userId, Guid bankLoanId) => BankLoanStatusChange(userId, bankLoanId, BankLoanStatus.Rejected);

  public async Task<BankLoanEntity> DeleteBankLoanById(Guid bankLoanId)
  {
    BankLoanEntity bankLoan = GetBankLoanById(bankLoanId);
    BankLoanEntity deletedBankLoan = _bankRepository.Delete(bankLoan);
    _ = await _context.SaveAsync();

    return deletedBankLoan;
  }

  public IAsyncEnumerable<BankLoanEntity> GetBankLoansByUserId(Guid userId)
  {
    CheckUserById(userId);
    var bankLoans = _bankRepository.GetByFilter(bankLoan => bankLoan.UserId == userId, bankLoan => bankLoan
      .OrderByDescending(order => order.PaymentTerm)
      .OrderBy(order => order.Status)
      .ThenBy(order => order.Amount))
      .ToAsyncEnumerable();

    return bankLoans;
  }

  public IAsyncEnumerable<BankLoanEntity> GetPendingBankLoans(Guid userId)
  {
    CheckUserById(userId);
    CheckUserPermission(userId);
    var bankLoans = _bankRepository.GetByFilter(bankLoan => bankLoan.Status == BankLoanStatus.Pending, bankLoan => bankLoan
      .OrderByDescending(order => order.PaymentTerm)
      .OrderBy(order => order.Status)
      .ThenBy(order => order.Amount))
      .ToAsyncEnumerable();

    return bankLoans;
  }

  public Task<BankLoanEntity> FindBankLoanById(Guid bankLoanId) => Task.FromResult(GetBankLoanById(bankLoanId));

  private void CheckUserById(Guid userId)
  {
    bool existingUser = _userRepository.Exists(user => user.UserId == userId);
    if (!existingUser)
      throw UserExceptionHelper.NotFound(userId);
  }

  private void CheckUserPermission(Guid userId)
  {
    UserEntity user = _userRepository.Find([userId]) ?? throw UserExceptionHelper.NotFound(userId);
    RoleEntity role = _roleRepository.Find([user.RoleId]) ?? throw RoleExceptionHelper.NotFound(user.RoleId);
    if (!RolesHelper.IsAdminUserById(role.RoleId))
      throw UserExceptionHelper.BadRequest(role.Name);
  }

  private BankLoanEntity GetBankLoanById(Guid bankLoanId)
  {
    BankLoanEntity bankLoan = _bankRepository.Find([bankLoanId]) ?? throw BankLoanExceptionHelper.NotFound(bankLoanId);

    return bankLoan;
  }

  private BankLoanEntity GetBankLoan(Guid userId, Guid bankLoanId)
  {
    BankLoanEntity bankLoan = _bankRepository.Find(bankLoan => bankLoan.UserId == bankLoan.UserId && bankLoan.BankLoanId == bankLoanId)
      ?? throw BankLoanExceptionHelper.NotFound(userId, bankLoanId);

    return bankLoan;
  }

  private async Task<BankLoanEntity> BankLoanStatusChange(Guid userId, Guid bankLoanId, BankLoanStatus status)
  {
    BankLoanEntity bankLoan = GetBankLoan(userId, bankLoanId);
    CheckUserPermission(bankLoan.UserId);
    if (bankLoan.Status != BankLoanStatus.Pending)
      throw BankLoanExceptionHelper.BadRequest(bankLoan.Status, status);
    bankLoan.Status = status;
    BankLoanEntity updatedBankLoan = _bankRepository.Update(bankLoan);
    _ = await _context.SaveAsync();

    return updatedBankLoan;
  }
}
