using AutoMapper;
using Makers.Dev.Contracts.DTO.Role;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.API.Mappers.Converters;

class UsersFilterConverter : ITypeConverter<IAsyncEnumerable<(RoleEntity Role, UserEntity? User)>, IAsyncEnumerable<UsersResult>>
{
  public async IAsyncEnumerable<UsersResult> Convert(
    IAsyncEnumerable<(RoleEntity Role, UserEntity? User)> source,
    IAsyncEnumerable<UsersResult> destination,
    ResolutionContext context)
  {
    IRuntimeMapper mapper = context.Mapper;
    var sources = await source.ToArrayAsync();
    var roles = sources.Select(source => source.Role);
    var users = sources.Select(source => source.User);
    var usersResult = roles
      .Distinct()
      .GroupJoin(
        users,
        role => role.RoleId,
        user => user?.RoleId,
        (role, users) => new UsersResult
        {
          Role = mapper.Map<RoleResponse>(role),
          Users = users
            .Distinct()
            .Select(mapper.Map<UserResponse>)
        });
    foreach (UsersResult userResult in usersResult)
      yield return userResult;
  }
}
