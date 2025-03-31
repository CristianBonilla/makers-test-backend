using AutoMapper;
using Makers.Dev.Contracts.DTO.Role;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.API.Mappers.Converters;

class UsersFilterConverter : ITypeConverter<IAsyncEnumerable<(RoleEntity Role, IEnumerable<UserEntity> Users)>, IAsyncEnumerable<UsersResult>>
{
  public async IAsyncEnumerable<UsersResult> Convert(
    IAsyncEnumerable<(RoleEntity Role, IEnumerable<UserEntity> Users)> source,
    IAsyncEnumerable<UsersResult> destination,
    ResolutionContext context)
  {
    IRuntimeMapper mapper = context.Mapper;

    await foreach (var (role, users) in source)
      yield return new()
      {
        Role = mapper.Map<RoleResponse>(role),
        Users = users.Select(mapper.Map<UserResponse>)
      };
  }
}
