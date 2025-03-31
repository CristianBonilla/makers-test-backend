using Makers.Dev.Domain.Entities;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.Contracts.Services;

public interface IBankLoanService
{
  Task<BankLoanEntity> AddBankLoan(BankLoanEntity bankLoan);
  Task<BankLoanEntity> ApproveBankLoan(Guid userId, Guid bankLoanId);
  Task<BankLoanEntity> RejectBankLoan(Guid userId, Guid bankLoanId);
  Task<BankLoanEntity> DeleteBankLoanById(Guid userId, Guid bankLoanId);
  (UserEntity User, IEnumerable<BankLoanEntity> BankLoans) GetBankLoansByUserId(Guid userId);
  IAsyncEnumerable<(UserEntity User, IEnumerable<BankLoanEntity> BankLoans)> GetPendingBankLoans();
  Task<BankLoanEntity> FindBankLoanById(Guid userId, Guid bankLoanId);
}
