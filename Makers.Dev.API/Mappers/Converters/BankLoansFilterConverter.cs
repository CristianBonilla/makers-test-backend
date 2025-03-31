using AutoMapper;
using Makers.Dev.Contracts.DTO.BankLoan;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Domain.Entities;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.API.Mappers.Converters;

class BankLoansFilterConverter : ITypeConverter<(UserEntity User, IEnumerable<BankLoanEntity> BankLoans), BankLoansResult>
{
  public BankLoansResult Convert(
    (UserEntity User, IEnumerable<BankLoanEntity> BankLoans) source,
    BankLoansResult destination,
    ResolutionContext context)
  {
    IRuntimeMapper mapper = context.Mapper;

    return new()
    {
      User = mapper.Map<UserResponse>(source.User),
      BankLoans = source.BankLoans.Select(mapper.Map<BankLoanResponse>)
    };
  }
}
