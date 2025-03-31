using AutoMapper;
using Makers.Dev.Contracts.DTO.Role;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.API.Mappers.Converters;

class RolesFilterConverter : ITypeConverter<IAsyncEnumerable<(RoleEntity Role, UserEntity? User)>, IAsyncEnumerable<RoleResult>>
{
  public async IAsyncEnumerable<RoleResult> Convert(
    IAsyncEnumerable<(RoleEntity Role, UserEntity? User)> source,
    IAsyncEnumerable<RoleResult> destination,
    ResolutionContext context)
  {
    IRuntimeMapper mapper = context.Mapper;
    var sources = await source.ToArrayAsync();
    var roles = sources.Select(source => source.Role);
    var users = sources.Select(source => source.User);
    var rolesResult = roles
      .Distinct()
      .GroupJoin(
        users,
        role => role.RoleId,
        user => user?.RoleId,
        (role, users) => new RoleResult
        {
          Role = mapper.Map<RoleResponse>(role),
          Users = users
            .Distinct()
            .Select(mapper.Map<UserResponse>)
        });
    foreach (RoleResult roleResult in rolesResult)
      yield return roleResult;
  }
}
