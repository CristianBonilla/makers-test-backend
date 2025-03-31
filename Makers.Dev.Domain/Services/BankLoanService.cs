using System.Linq.Expressions;
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

  public Task<BankLoanEntity> ApproveBankLoan(Guid superUserId, Guid userId, Guid bankLoanId) => BankLoanStatusChange(superUserId, userId, bankLoanId, BankLoanStatus.Approved);

  public Task<BankLoanEntity> RejectBankLoan(Guid superUserId, Guid userId, Guid bankLoanId) => BankLoanStatusChange(superUserId, userId, bankLoanId, BankLoanStatus.Rejected);

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

  public IAsyncEnumerable<(UserEntity User, IEnumerable<BankLoanEntity> BankLoans)> GetPendingBankLoans(Guid superUserId)
  {
    CheckUserById(superUserId);
    CheckUserPermission(superUserId);
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

  private void CheckUserPermission(Guid userId)
  {
    UserEntity user = _userRepository.Find([userId]) ?? throw UserExceptionHelper.NotFound(userId);
    RoleEntity role = _roleRepository.Find([user.RoleId]) ?? throw RoleExceptionHelper.NotFound(user.RoleId);
    if (!RolesHelper.IsAdminUserById(role.RoleId))
      throw UserExceptionHelper.BadRequest(role.Name);
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

  private async Task<BankLoanEntity> BankLoanStatusChange(Guid superUserId, Guid userId, Guid bankLoanId, BankLoanStatus status)
  {
    CheckUserById(superUserId);
    CheckUserPermission(superUserId);
    BankLoanEntity bankLoan = GetBankLoan(userId, bankLoanId);
    if (bankLoan.Status != BankLoanStatus.Pending)
      throw BankLoanExceptionHelper.BadRequest(bankLoan.Status, status);
    bankLoan.Status = status;
    BankLoanEntity updatedBankLoan = _bankRepository.Update(bankLoan);
    _ = await _context.SaveAsync();

    return updatedBankLoan;
  }
}
