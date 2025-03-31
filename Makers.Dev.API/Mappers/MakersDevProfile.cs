using AutoMapper;
using Makers.Dev.API.Mappers.Converters;
using Makers.Dev.Contracts.DTO.BankLoan;
using Makers.Dev.Domain.Entities;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.API.Mappers;

class MakersDevProfile : Profile
{
  public MakersDevProfile()
  {
    CreateMap<BankLoanRequest, BankLoanEntity>()
      .ForMember(member => member.BankLoanId, options => options.Ignore())
      .ForMember(member => member.Status, options => options.Ignore())
      .ForMember(member => member.Created, options => options.Ignore())
      .ForMember(member => member.Version, options => options.Ignore())
      .ForMember(member => member.User, options => options.Ignore());
    CreateMap<BankLoanEntity, BankLoanResponse>()
      .ReverseMap()
      .ForMember(member => member.Version, options => options.Ignore())
      .ForMember(member => member.User, options => options.Ignore());
    CreateMap<(UserEntity User, IEnumerable<BankLoanEntity> BankLoans), BankLoansResult>()
      .ConvertUsing<BankLoansFilterConverter>();
  }
}
