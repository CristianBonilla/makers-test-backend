using Makers.Dev.Contracts.Repository;
using Makers.Dev.Domain.Entities;
using Makers.Dev.Infrastructure.Contexts.MakersDev;

namespace Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

public interface IBankLoanRepository : IRepository<MakersDevContext, BankLoanEntity> { }
