using Makers.Dev.Domain.Entities;
using Makers.Dev.Infrastructure.Contexts.MakersDev;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.Infrastructure.Repositories.MakersDev;

public class BankLoanRepository(IMakersDevRepositoryContext context) : Repository<MakersDevContext, BankLoanEntity>(context), IBankLoanRepository { }
