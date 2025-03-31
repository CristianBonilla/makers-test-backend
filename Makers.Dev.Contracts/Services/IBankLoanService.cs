using Makers.Dev.Domain.Entities;

namespace Makers.Dev.Contracts.Services;

public interface IBankLoanService
{
  Task<BankLoanEntity> AddBankLoan(BankLoanEntity bankLoan);
  Task<BankLoanEntity> ApproveBankLoan(Guid bankLoanId);
  Task<BankLoanEntity> RejectBankLoan(Guid bankLoanId);
  IAsyncEnumerable<BankLoanEntity> GetPendingBankLoans(Guid userId);
  IAsyncEnumerable<BankLoanEntity> GetBankLoansByUserId(Guid userId);
  Task<BankLoanEntity> FindBankLoanById(Guid bankLoanId); 
}
