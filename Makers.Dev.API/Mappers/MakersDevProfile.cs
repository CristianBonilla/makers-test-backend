using AutoMapper;
using Makers.Dev.Contracts.DTO.BankLoan;
using Makers.Dev.Domain.Entities;

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
  }
}
