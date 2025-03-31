using Makers.Dev.Infrastructure.Contexts.MakersDev;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.Infrastructure.Repositories.MakersDev;

public class MakersDevRepositoryContext(MakersDevContext context) : RepositoryContext<MakersDevContext>(context), IMakersDevRepositoryContext { }
