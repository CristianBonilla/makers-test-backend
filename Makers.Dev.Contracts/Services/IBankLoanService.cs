using Makers.Dev.Domain.Entities;

namespace Makers.Dev.Contracts.Services;

public interface IBankLoanService
{
  Task<BankLoanEntity> AddBankLoan(BankLoanEntity bankLoan);
  Task<BankLoanEntity> ApproveBankLoan(Guid userId, Guid bankLoanId);
  Task<BankLoanEntity> RejectBankLoan(Guid userId, Guid bankLoanId);
  Task<BankLoanEntity> DeleteBankLoanById(Guid bankLoanId);
  IAsyncEnumerable<BankLoanEntity> GetBankLoansByUserId(Guid userId);
  IAsyncEnumerable<BankLoanEntity> GetPendingBankLoans(Guid userId);
  Task<BankLoanEntity> FindBankLoanById(Guid bankLoanId); 
}
