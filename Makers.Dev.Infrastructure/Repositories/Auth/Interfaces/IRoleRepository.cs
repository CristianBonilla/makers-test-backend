using Makers.Dev.Contracts.Repository;
using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Infrastructure.Contexts.MakersDev;

namespace Makers.Dev.Infrastructure.Repositories.Auth.Interfaces;

public interface IRoleRepository : IRepository<MakersDevContext, RoleEntity> { }
