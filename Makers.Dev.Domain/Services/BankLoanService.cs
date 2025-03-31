using System.Linq.Expressions;
using Makers.Dev.Contracts.Services;
using Makers.Dev.Domain.Entities;
using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Domain.Helpers.Exceptions;
using Makers.Dev.Infrastructure.Repositories.Auth.Interfaces;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.Domain.Services;

public class BankLoanService(
  IMakersDevRepositoryContext context,
  IBankLoanRepository bankLoanRepository,
  IUserRepository userRepository) : IBankLoanService
{
  readonly IMakersDevRepositoryContext _context = context;
  readonly IBankLoanRepository _bankRepository = bankLoanRepository;
  readonly IUserRepository _userRepository = userRepository;

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

  public async Task<BankLoanEntity> DeleteBankLoanById(Guid userId, Guid bankLoanId)
  {
    BankLoanEntity bankLoan = GetBankLoan(userId, bankLoanId);
    BankLoanEntity deletedBankLoan = _bankRepository.Delete(bankLoan);
    _ = await _context.SaveAsync();

    return deletedBankLoan;
  }

  public (UserEntity User, IEnumerable<BankLoanEntity> BankLoans) GetBankLoansByUserId(Guid userId)
  {
    UserEntity user = GetUserById(userId);

    return (user, GetBankLoans(bankLoan => bankLoan.UserId == userId));
  }

  public IAsyncEnumerable<(UserEntity User, IEnumerable<BankLoanEntity> BankLoans)> GetPendingBankLoans()
  {
    var bankLoans = _userRepository
      .GetAll()
      .GroupJoin(
        GetBankLoans(bankLoan => bankLoan.Status == BankLoanStatus.Pending),
        user => user.UserId,
        bankLoan => bankLoan.UserId,
        (user, bankLoans) => (user, bankLoans))
      .Where(pending => pending.bankLoans.Any())
      .ToAsyncEnumerable();

    return bankLoans;
  }

  public Task<BankLoanEntity> FindBankLoanById(Guid userId, Guid bankLoanId) => Task.FromResult(GetBankLoan(userId, bankLoanId));

  private void CheckUserById(Guid userId)
  {
    bool existingUser = _userRepository.Exists(user => user.UserId == userId);
    if (!existingUser)
      throw UserExceptionHelper.NotFound(userId);
  }

  private BankLoanEntity GetBankLoan(Guid userId, Guid bankLoanId)
  {
    BankLoanEntity bankLoan = _bankRepository.Find(bankLoan => bankLoan.UserId == userId && bankLoan.BankLoanId == bankLoanId)
      ?? throw BankLoanExceptionHelper.NotFound(userId, bankLoanId);

    return bankLoan;
  }

  private UserEntity GetUserById(Guid userId)
  {
    UserEntity user = _userRepository.Find([userId]) ?? throw UserExceptionHelper.NotFound(userId);

    return user;
  }

  private IEnumerable<BankLoanEntity> GetBankLoans(Expression<Func<BankLoanEntity, bool>> filter)
  {
    var bankLoans = _bankRepository.GetByFilter(filter, bankLoan => bankLoan
      .OrderByDescending(order => order.PaymentTerm)
      .OrderBy(order => order.Status)
      .ThenBy(order => order.Amount));

    return bankLoans;
  }

  private async Task<BankLoanEntity> BankLoanStatusChange(Guid userId, Guid bankLoanId, BankLoanStatus status)
  {
    BankLoanEntity bankLoan = GetBankLoan(userId, bankLoanId);
    if (bankLoan.Status != BankLoanStatus.Pending)
      throw BankLoanExceptionHelper.BadRequest(bankLoan.Status, status);
    bankLoan.Status = status;
    BankLoanEntity updatedBankLoan = _bankRepository.Update(bankLoan);
    _ = await _context.SaveAsync();

    return updatedBankLoan;
  }
}
