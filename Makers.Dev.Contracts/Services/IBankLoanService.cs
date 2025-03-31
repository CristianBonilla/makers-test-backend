using Makers.Dev.Domain.Entities;

namespace Makers.Dev.Contracts.Services;

public interface IBankLoanService
{
  Task<BankLoanEntity> AddBankLoan(BankLoanEntity bankLoan);
  Task<BankLoanEntity> UpdateBankLoan(BankLoanEntity bankLoan);
  Task<BankLoanEntity> UpdateBankLoanStatus(Guid bankLoanId, BankLoanStatus bankLoanStatus);
  Task<BankLoanEntity> DeleteBankLoanById(Guid bankLoanId);
  IAsyncEnumerable<BankLoanEntity> GetBankLoans();
  IAsyncEnumerable<BankLoanEntity> GetBanLoansByStatusByUserId(Guid userId, BankLoanStatus bankLoanStatus);
  Task<BankLoanEntity> FindBankLoanById(Guid bankLoanId);
}
