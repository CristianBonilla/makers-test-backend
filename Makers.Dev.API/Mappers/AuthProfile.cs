using AutoMapper;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.API.Mappers;

class AuthProfile : Profile
{
  public AuthProfile()
  {
    CreateMap<UserRegisterRequest, UserEntity>()
      .ForMember(member => member.UserId, options => options.Ignore())
      .ForMember(member => member.IsActive, options => options.Ignore())
      .ForMember(member => member.Salt, options => options.Ignore())
      .ForMember(member => member.Created, options => options.Ignore())
      .ForMember(member => member.Version, options => options.Ignore())
      .ForMember(member => member.Role, options => options.Ignore())
      .ForMember(member => member.BankLoans, options => options.Ignore());
    CreateMap<UserEntity, UserResponse>()
      .ReverseMap()
      .ForMember(member => member.Password, options => options.Ignore())
      .ForMember(member => member.Salt, options => options.Ignore())
      .ForMember(member => member.Version, options => options.Ignore())
      .ForMember(member => member.Role, options => options.Ignore())
      .ForMember(member => member.BankLoans, options => options.Ignore());
  }
}
