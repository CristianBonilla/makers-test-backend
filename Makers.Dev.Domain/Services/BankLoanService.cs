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

  public async Task<BankLoanEntity> UpdateBankLoan(BankLoanEntity bankLoan)
  {
    CheckBankLoanById(bankLoan.BankLoanId);
    CheckUserPermission(bankLoan.UserId);
    BankLoanEntity updatedBankLoan = _bankRepository.Update(bankLoan);
    _ = await _context.SaveAsync();

    return updatedBankLoan;
  }

  public async Task<BankLoanEntity> UpdateBankLoanStatus(Guid bankLoanId, BankLoanStatus bankLoanStatus)
  {
    BankLoanEntity bankLoan = GetBankLoanById(bankLoanId);
    CheckUserPermission(bankLoan.UserId);
    bankLoan.Status = bankLoanStatus;
    BankLoanEntity updatedBankLoan = _bankRepository.Update(bankLoan);
    _ = await _context.SaveAsync();

    return updatedBankLoan;
  }

  public async Task<BankLoanEntity> DeleteBankLoanById(Guid bankLoanId)
  {
    BankLoanEntity bankLoan = GetBankLoanById(bankLoanId);
    CheckUserPermission(bankLoan.UserId);
    BankLoanEntity deletedBankLoan = _bankRepository.Delete(bankLoan);
    _ = await _context.SaveAsync();

    return deletedBankLoan;
  }

  public IAsyncEnumerable<BankLoanEntity> GetBankLoans()
  {
    var bankLoans = _bankRepository.GetAll(bankLoan => bankLoan
      .OrderByDescending(order => order.PaymentTerm)
      .OrderBy(order => order.Status)
      .ThenBy(order => order.Amount))
      .ToAsyncEnumerable();

    return bankLoans;
  }

  public IAsyncEnumerable<BankLoanEntity> GetBanLoansByStatusByUserId(Guid userId, BankLoanStatus bankLoanStatus)
  {
    CheckUserById(userId);
    var bankLoans = _bankRepository.GetByFilter(bankLoan => bankLoan.UserId == userId && bankLoan.Status == bankLoanStatus, bankLoan => bankLoan
      .OrderByDescending(order => order.PaymentTerm)
      .OrderBy(order => order.Status)
      .ThenBy(order => order.Amount))
      .ToAsyncEnumerable();

    return bankLoans;
  }

  public Task<BankLoanEntity> FindBankLoanById(Guid bankLoanId) => Task.FromResult(GetBankLoanById(bankLoanId));

  private void CheckBankLoanById(Guid bankLoanId)
  {
    bool existingBankLoan = _bankRepository.Exists(bankLoan => bankLoan.BankLoanId == bankLoanId);
    if (!existingBankLoan)
      throw BankLoanExceptionHelper.NotFound(bankLoanId);
  }

  private void CheckUserById(Guid userId)
  {
    bool existingUser = _userRepository.Exists(user => user.UserId == userId);
    if (!existingUser)
      throw UserExceptionHelper.NotFound(userId);
  }

  private void CheckUserPermission(Guid userId)
  {
    Guid roleId = GetRoleIdByUserId(userId);
    RoleEntity role = GetRoleById(roleId);
    if (!RolesHelper.IsAdminUserById(role.RoleId))
      throw UserExceptionHelper.BadRequest(role.Name);
  }

  private BankLoanEntity GetBankLoanById(Guid bankLoanId)
  {
    BankLoanEntity bankLoan = _bankRepository.Find([bankLoanId]) ?? throw BankLoanExceptionHelper.NotFound(bankLoanId);

    return bankLoan;
  }

  private RoleEntity GetRoleById(Guid roleId)
  {
    RoleEntity role = _roleRepository.Find([roleId]) ?? throw RoleExceptionHelper.NotFound(roleId);

    return role;
  }

  private Guid GetRoleIdByUserId(Guid userId)
  {
    UserEntity user = _userRepository.Find([userId]) ?? throw UserExceptionHelper.NotFound(userId);

    return user.RoleId;
  }
}
