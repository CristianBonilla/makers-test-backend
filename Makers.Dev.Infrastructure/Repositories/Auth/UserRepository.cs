using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Infrastructure.Contexts.MakersDev;
using Makers.Dev.Infrastructure.Repositories.Auth.Interfaces;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.Infrastructure.Repositories.Auth;

public class UserRepository(IMakersDevRepositoryContext context) : Repository<MakersDevContext, UserEntity>(context), IUserRepository { }
